using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;


// DOTweenList
// https://blog.gigacreation.jp/entry/2019/11/27/203924

// 【DOTween/UniTask】Forgetした後にDestroyすると警告が出る
// https://takap-tech.com/entry/2022/07/21/020225

namespace yamap_BoardGame {

    [RequireComponent(typeof(Charactor))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class CharactorMover : MonoBehaviour {

        // インスタンシエイトした場合には、メソッドを利用して外部からもらう

        //[SerializeField]
        //private DiceRollObserver diceRollObserver;

        //[SerializeField]
        //private Field field;

        private PlayerAnimation playerAnimation;
        private Charactor chara;    // Player のときと Opponent のときはアサインを逆にする
        private float walkAnimSpeed = 0.5f;

        //[SerializeField]
        //private Charactor opponent;

        public Charactor Chara { get => chara; }


        public void SetUp() {
            if (TryGetComponent(out playerAnimation)) {
                playerAnimation.MoveAnimation(0);
            }

            if(TryGetComponent(out chara)) {
                chara.placeNo = 0;
            }

            //chara.IsMyTurn.Value = chara.ownerType == OwnerType.Player ? true : false;
            //opponent.IsMyTurn.Value = opponent.ownerType == OwnerType.Opponent ? false : true;

            //diceRollObserver?.DiceRoll
            //    .Where(_ => chara.IsMyTurn.Value == true)
            //    .Subscribe(value => MoveAsync(value, this.GetCancellationTokenOnDestroy()).Forget()).AddTo(this);

            //// 敵の場合
            //if (chara.ownerType == OwnerType.Opponent) {
            //    opponent.IsMyTurn.Where(x => x == true)
            //        .Subscribe(_ => diceRollObserver.RollDice(6))
            //        .AddTo(this);
            //}
        }

        /// <summary>
        /// キャラの移動
        /// </summary>
        /// <param name="moveConut"></param>
        /// <returns></returns>
        public async UniTask<Panel> MoveAsync(int moveConut, CancellationToken token, EventChecker eventChecker, DiceRollObserver diceRollObserver, Field field) {   // 0 の出目もあり。その場合は移動しない

            Debug.Log(moveConut);
            if (moveConut == 0) {
                // 自分のパネルの場合には増産できるようにする
                return field.panels[chara.placeNo];
            }

            //// 自分のターン終了
            //chara.IsMyTurn.Value = false;


            // TODO 分岐パネルにいる場合には、最初に移動する方向を選択する　await すること


            playerAnimation.MoveAnimation(walkAnimSpeed);

            DOTweenList dotweenList = new ();
            
            Sequence sequence = DOTween.Sequence();

            for (int i = 1; i <= moveConut; i++) {

                int nextPlace = i + chara.placeNo;
                if (nextPlace >= field.panels.Length) {
                    nextPlace -= field.panels.Length;
                }

                Vector3 nextPos = field.panels[nextPlace].transform.localPosition;

                dotweenList.Add(AppendMove(sequence, nextPos, field.panels[nextPlace].directionType, field.panels.Length));

                //await AppendMoveAsync(sequence, nextPos);

                //_ = sequence.Append(transform.DOLocalMove(nextPos, 0.5f).SetEase(Ease.InQuart))
                //   .SetLink(gameObject)
                //   .OnComplete(() => {
                //       chara.placeNo = GetNewPlaceNo(chara.placeNo);
                //       ChangeDirection(chara.placeNo);
                //   });
            }
            // List の処理がすべて終了するまで待機。Sequence にしてあるので、順番に処理される
            await dotweenList.PlayForward();

            //await sequence.AppendInterval((moveConut - 1) * 0.3f)
            //    .SetLink(gameObject);
                //.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, this.GetCancellationTokenOnDestroy());
            
            playerAnimation.MoveAnimation(0);

            //// イベント判定(マーカー作成と Hp 減少)
            //Marker marker = await eventChecker.CheckEventAsync(chara, field.panels[chara.placeNo]);

            //if (marker != null) {               
            //    field.panels[chara.placeNo].markerList.Add(marker);
            //}

            //await UniTask.Delay(1500, cancellationToken: token);


            //opponent.IsMyTurn.Value = true;


            Debug.Log("移動終了");

            return field.panels[chara.placeNo];

            // 敵のターンの終了まで待機
            //await UniTask.WaitUntil(() => opponent.IsMyTurn.Value == false, cancellationToken: token);

            // ボタンを再度押せるようにする


            //Debug.Log("プレイヤーのターン開始");
        }

        /// <summary>
        /// キャラの移動処理用。未使用
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private async UniTask AppendMoveAsync(Sequence sequence, Vector3 newPos, DirectionType newDirectionType, int fieldCount) {
           await sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ←　ここでさらに ) して閉じないこと。その場合、OnComplete が最後にしか呼ばれなくなる
                .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo, fieldCount);
                     ChangeDirection(newDirectionType);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// DOTweenList 用(DOTweenList 自体には Sequence 機能はないので、追加した順番に処理したい場合には Sequence を自前で用意する)
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private Tween AppendMove(Sequence sequence, Vector3 newPos, DirectionType newDirectionType, int fieldCount) {
            return sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ←　ここでさらに ) して閉じないこと。その場合、OnComplete が最後にしか呼ばれなくなる
                 .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo, fieldCount);
                     Debug.Log(chara.placeNo);

                     // キャラの向きを進行に合わせる
                     ChangeDirection(newDirectionType);

                     // TODO イベント

                 }));
        }

        /// <summary>
        /// キャラのいるパネルの番号を更新
        /// </summary>
        /// <param name="placeNo"></param>
        /// <returns></returns>
        private int GetNewPlaceNo(int placeNo, int fieldCount) {
            int newPlaceNo = placeNo;

            if (fieldCount - 1 != placeNo) {
                newPlaceNo++;
            } else {
                newPlaceNo = 0;
            }
            return newPlaceNo;
        }

        /// <summary>
        /// キャラの向きの更新
        /// </summary>
        /// <param name="placeNo"></param>
        private void ChangeDirection(DirectionType nextDirectionType) {
            if (nextDirectionType == DirectionType.None) {
                return;
            }
            transform.DORotate(new(transform.position.x, GetRotateAngle(nextDirectionType), transform.position.z), 0.5f).SetEase(Ease.Linear);
        }

        /// <summary>
        /// キャラの向きを進行方向に合わせる
        /// </summary>
        /// <param name="nextDirectionType"></param>
        /// <returns></returns>
        private float GetRotateAngle(DirectionType nextDirectionType) {
            // モデルの初期アングルによって値を変更すること。ここでは 90 の角度で正面方向を進むようにしているケース
            return nextDirectionType switch {
                DirectionType.Front => 90,
                DirectionType.Back => -90,
                DirectionType.Right => 180,
                DirectionType.Left =>  0,
                _ => 0
            };
        }
    }
}