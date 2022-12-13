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

            // �{�^���̃C�x���g�w��
            Button btn = rollDiceDispatcher.DispatchRollButton(maxDice, diceRollObserver);
            btn.interactable = false;

            // �_�C�X�̍w�ǁB�o�ڂ��ς�邽�тɏo�ڂ̕\���X�V����
            diceRollObserver.DiceRoll
                .Subscribe(value => {
                    diceRollViewer.ShowDiceRoll(value);
                })        
                .AddTo(this);

            var token = this.GetCancellationTokenOnDestroy();

            player.SetUp();

            // �v���C���[
            diceRollObserver.DiceRoll
                .Where(_ => player.Chara.IsMyTurn.Value && !opponent.Chara.IsMyTurn.Value)
                .Subscribe(async value =>
                {
                    btn.interactable = false;
                    Panel movedStopPanel = await player.MoveAsync(value, token, eventChecker, diceRollObserver, field);�@�@�@// Field �� Observer�@���n���悤�ɂ��ă����o�ϐ������
                    await eventChecker.CheckEventAsync(player.Chara, movedStopPanel);

                    await UniTask.Delay(500, cancellationToken: token);
                    

                    //Debug.Log("opponent.Chara.IsMyTurn.Value : " + opponent.Chara.IsMyTurn.Value);
                    // �J����
                    cameraManager.SwitchCharaCamera((int)opponent.Chara.ownerType);
                    await UniTask.Delay(250, cancellationToken: token);
                    player.Chara.IsMyTurn.Value = false;
                })
                .AddTo(this);

            opponent.SetUp();

            // �G
            diceRollObserver.DiceRoll
                .Where(_ => opponent.Chara.IsMyTurn.Value && !player.Chara.IsMyTurn.Value)
                .Subscribe(async value => {
                    Panel movedStopPanel = await opponent.MoveAsync(value, token, eventChecker, diceRollObserver, field);
                    await eventChecker.CheckEventAsync(opponent.Chara, movedStopPanel);

                    await UniTask.Delay(500, cancellationToken: token);
                    

                    // �J����
                    cameraManager.SwitchCharaCamera((int)player.Chara.ownerType);
                    await UniTask.Delay(250, cancellationToken: token);
                    opponent.Chara.IsMyTurn.Value = false;
                })
                .AddTo(this);

            // �G�������ŃT�C�R����U��
            opponent.Chara.IsMyTurn
                .Where(_ => opponent.Chara.IsMyTurn.Value && !player.Chara.IsMyTurn.Value)
                .Subscribe(value => diceRollObserver.RollDice(maxDice))
                .AddTo(this);

            // �X�^�[�g���̏o�ڕ\���X�V
            diceRollViewer.ShowDiceRoll(0);

            // HP �̍w��
            player.Chara.Hp
                .Where(value => value <= 0)
                .Subscribe(_ => gameStateModel.CurrentGameState.Value = GameState.GameOver)�@�@�@// Debug.Log("Failed...")
                .AddTo(this);

            opponent.Chara.Hp
                .Where(value => value <= 0)
                .Subscribe(_ => gameStateModel.CurrentGameState.Value = GameState.Clear)    // Debug.Log("Game Clear!")
                .AddTo(this);

            // �����Őݒ肵�Ȃ��ƓG�������ɍw�ǂ��ď���ɓ����Ă��܂�
            player.Chara.IsMyTurn.Value = true;
            btn.interactable = true;

            // �s���I���̍w��
            player.Chara.IsMyTurn
                .Where(x => x == false)
                .Subscribe(async _ => {
                    if (player.Chara.Hp.Value <= 0) {
                        Debug.Log("Failed...");
                    } else {
                        await UniTask.Delay(500, cancellationToken: token);
                        opponent.Chara.IsMyTurn.Value = true;
                        Debug.Log("�G�̃^�[����");
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
                        Debug.Log("�v���C���[�̃^�[����");
                        btn.interactable = true;
                    }
                })
                .AddTo(this);

            // State �w��
            gameStateModel.CurrentGameState
                .Where(state => state == GameState.Clear)
                .Subscribe(_ => Debug.Log("�Q�[���N���A���o")).AddTo(this);  // �J�������v���[���[�̃J�����ɕς��āA�������o

            gameStateModel.CurrentGameState
                .Where(state => state == GameState.GameOver)
                .Subscribe(_ => Debug.Log("�Q�[���I�[�o�[���o")).AddTo(this);


            gameStateModel.CurrentGameState.Value = GameState.Play;
        }
    }
}