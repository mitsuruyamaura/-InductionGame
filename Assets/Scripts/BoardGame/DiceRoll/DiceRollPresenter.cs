using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace yamap_BoardGame {

    [RequireComponent(typeof(DiceRollObserver))]
    public class DiceRollPresenter : MonoBehaviour {

        [SerializeField]
        private DiceRollButtonDispatcher rollDiceDispatcher;

        [SerializeField]
        private DiceRollViewer diceRollViewer;

        [SerializeField]
        private Field field;

        [SerializeField]
        private int maxDice = 6;

        [SerializeField]
        private CharactorMover player;

        [SerializeField]
        private CharactorMover opponent;

        [SerializeField]
        private EventChecker eventChecker;

        [SerializeField]
        private GameStateModel gameStateModel;

        [SerializeField]
        private CameraManager cameraManager;


        private DiceRollObserver diceRollObserver;


        async UniTask Start() {
            DG.Tweening.DOTween.Init();

            TryGetComponent(out diceRollObserver);
            diceRollViewer.InitViewer();
            gameStateModel.CurrentGameState.Value = GameState.Wait;

            // ボタンのイベント購読
            Button btn = rollDiceDispatcher.DispatchRollButton(maxDice, diceRollObserver);
            btn.interactable = false;

            // ダイスの購読。出目が変わるたびに出目の表示更新する
            diceRollObserver.DiceRoll
                .Subscribe(value => {
                    diceRollViewer.ShowDiceRoll(value);
                })        
                .AddTo(this);

            var token = this.GetCancellationTokenOnDestroy();

            player.SetUp();

            // プレイヤー
            diceRollObserver.DiceRoll
                .Where(_ => player.Chara.IsMyTurn.Value && !opponent.Chara.IsMyTurn.Value)
                .Subscribe(async value =>
                {
                    btn.interactable = false;
                    Panel movedStopPanel = await player.MoveAsync(value, token, eventChecker, diceRollObserver, field);　　　// Field と Observer　も渡すようにしてメンバ変数を削る
                    await eventChecker.CheckEventAsync(player.Chara, movedStopPanel);

                    await UniTask.Delay(500, cancellationToken: token);
                    

                    //Debug.Log("opponent.Chara.IsMyTurn.Value : " + opponent.Chara.IsMyTurn.Value);
                    // カメラ
                    cameraManager.SwitchCharaCamera((int)opponent.Chara.ownerType);
                    await UniTask.Delay(250, cancellationToken: token);
                    player.Chara.IsMyTurn.Value = false;
                })
                .AddTo(this);

            opponent.SetUp();

            // 敵
            diceRollObserver.DiceRoll
                .Where(_ => opponent.Chara.IsMyTurn.Value && !player.Chara.IsMyTurn.Value)
                .Subscribe(async value => {
                    Panel movedStopPanel = await opponent.MoveAsync(value, token, eventChecker, diceRollObserver, field);
                    await eventChecker.CheckEventAsync(opponent.Chara, movedStopPanel);

                    await UniTask.Delay(500, cancellationToken: token);
                    

                    // カメラ
                    cameraManager.SwitchCharaCamera((int)player.Chara.ownerType);
                    await UniTask.Delay(250, cancellationToken: token);
                    opponent.Chara.IsMyTurn.Value = false;
                })
                .AddTo(this);

            // 敵が自動でサイコロを振る
            opponent.Chara.IsMyTurn
                .Where(_ => opponent.Chara.IsMyTurn.Value && !player.Chara.IsMyTurn.Value)
                .Subscribe(value => diceRollObserver.RollDice(maxDice))
                .AddTo(this);

            // スタート時の出目表示更新
            diceRollViewer.ShowDiceRoll(0);

            // HP の購読
            player.Chara.Hp
                .Where(value => value <= 0)
                .Subscribe(_ => gameStateModel.CurrentGameState.Value = GameState.GameOver)　　　// Debug.Log("Failed...")
                .AddTo(this);

            opponent.Chara.Hp
                .Where(value => value <= 0)
                .Subscribe(_ => gameStateModel.CurrentGameState.Value = GameState.Clear)    // Debug.Log("Game Clear!")
                .AddTo(this);

            // ここで設定しないと敵がすぐに購読して勝手に動いてしまう
            player.Chara.IsMyTurn.Value = true;
            btn.interactable = true;

            // 行動終了の購読
            player.Chara.IsMyTurn
                .Where(x => x == false)
                .Subscribe(async _ => {
                    if (player.Chara.Hp.Value <= 0) {
                        Debug.Log("Failed...");
                    } else {
                        await UniTask.Delay(500, cancellationToken: token);
                        opponent.Chara.IsMyTurn.Value = true;
                        Debug.Log("敵のターンへ");
                    }
                })
                .AddTo(this);

            opponent.Chara.IsMyTurn
                .Where(x => x == false)
                .Subscribe(async _ => {
                    if (opponent.Chara.Hp.Value <= 0) {
                        Debug.Log("Game Clear!");
                    } else {
                        await UniTask.Delay(500, cancellationToken: token);
                        player.Chara.IsMyTurn.Value = true;
                        Debug.Log("プレイヤーのターンへ");
                        btn.interactable = true;
                    }
                })
                .AddTo(this);

            // State 購読
            gameStateModel.CurrentGameState
                .Where(state => state == GameState.Clear)
                .Subscribe(_ => Debug.Log("ゲームクリア演出")).AddTo(this);  // カメラをプレーヤーのカメラに変えて、勝利演出

            gameStateModel.CurrentGameState
                .Where(state => state == GameState.GameOver)
                .Subscribe(_ => Debug.Log("ゲームオーバー演出")).AddTo(this);


            gameStateModel.CurrentGameState.Value = GameState.Play;
        }
    }
}