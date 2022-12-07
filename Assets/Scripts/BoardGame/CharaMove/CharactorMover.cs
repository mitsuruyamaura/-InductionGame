using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;


// DOTweenList
// https://blog.gigacreation.jp/entry/2019/11/27/203924

// 【DOTween/UniTask】Forgetした後にDestroyすると警告が出る
// https://takap-tech.com/entry/2022/07/21/020225

namespace yamap_BoardGame {

    [RequireComponent(typeof(Charactor))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class CharactorMover : MonoBehaviour {

        // インスタンシエイトした場合には、メソッドを利用して外部からもらう

        [SerializeField]
        private DiceRollObserver diceRollObserver;

        [SerializeField]
        private Field field;

        private PlayerAnimation playerAnimation;
        private Charactor chara;
        private float walkAnimSpeed = 0.5f;


        void Start() {
            if (TryGetComponent(out playerAnimation)) {
                playerAnimation.MoveAnimation(0);
            }

            if(TryGetComponent(out chara)) {
                chara.placeNo = 0;
            }

            diceRollObserver?.DiceRoll.Subscribe(value => MoveAsync(value).Forget()).AddTo(this);
        }

        /// <summary>
        /// キャラの移動
        /// </summary>
        /// <param name="moveConut"></param>
        /// <returns></returns>
        private async UniTask MoveAsync(int moveConut) {   // 0 の出目もあり。その場合は移動しない

            Debug.Log(moveConut);
            if (moveConut == 0) {
                return;
            }

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

                dotweenList.Add(AppendMove(sequence, nextPos, field.panels[nextPlace].directionType));

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

            // ボタンを再度押せるようにする

            Debug.Log("移動終了");
        }

        /// <summary>
        /// キャラの移動処理用。未使用
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private async UniTask AppendMoveAsync(Sequence sequence, Vector3 newPos, DirectionType newDirectionType) {
           await sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ←　ここでさらに ) して閉じないこと。その場合、OnComplete が最後にしか呼ばれなくなる
                .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
                     ChangeDirection(newDirectionType);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// DOTweenList 用(DOTweenList 自体には Sequence 機能はないので、追加した順番に処理したい場合には Sequence を自前で用意する)
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private Tween AppendMove(Sequence sequence, Vector3 newPos, DirectionType newDirectionType) {
            return sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ←　ここでさらに ) して閉じないこと。その場合、OnComplete が最後にしか呼ばれなくなる
                 .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
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
        private int GetNewPlaceNo(int placeNo) {
            int newPlaceNo = placeNo;

            if (field.panels.Length - 1 != placeNo) {
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