using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;


// DOTweenList
// https://blog.gigacreation.jp/entry/2019/11/27/203924

// �yDOTween/UniTask�zForget�������Destroy����ƌx�����o��
// https://takap-tech.com/entry/2022/07/21/020225

namespace yamap_BoardGame {

    [RequireComponent(typeof(Chara))]
    public class CharactorRenderer : MonoBehaviour {

        [SerializeField]
        private DiceRollObserver diceRollObserver;

        [SerializeField]
        private Field field;

        private PlayerAnimation playerAnimation;
        private Chara chara;
        private float walkAnimSpeed = 0.5f;


        void Start() {
            if (TryGetComponent(out playerAnimation)) {
                playerAnimation.MoveAnimation(0);
            }

            TryGetComponent(out chara);
            diceRollObserver.DiceRoll.Subscribe(value => MoveAsync(value).Forget()).AddTo(this);
        }


        private async UniTask MoveAsync(int moveConut) {
            playerAnimation.MoveAnimation(walkAnimSpeed);

            DOTweenList dotweenList = new ();
            
            Sequence sequence = DOTween.Sequence();

            for (int i = 1; i <= moveConut + 1; i++) {

                int nextPlace = i + chara.placeNo;
                if (nextPlace >= field.panels.Length) {
                    nextPlace -= field.panels.Length;
                }

                Vector3 nextPos = field.panels[nextPlace].transform.localPosition;

                dotweenList.Add(AppendMove(sequence, nextPos));

                //await AppendMoveAsync(sequence, nextPos);

                //_ = sequence.Append(transform.DOLocalMove(nextPos, 0.5f).SetEase(Ease.InQuart))
                //   .SetLink(gameObject)
                //   .OnComplete(() => {
                //       chara.placeNo = GetNewPlaceNo(chara.placeNo);
                //       ChangeDirection(chara.placeNo);
                //   });
            }
            await dotweenList.PlayForward();

            //await sequence.AppendInterval((moveConut - 1) * 0.3f)
            //    .SetLink(gameObject);
                //.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, this.GetCancellationTokenOnDestroy());
            
            playerAnimation.MoveAnimation(0);

            // �{�^�����ēx������悤�ɂ���

            Debug.Log("�ړ��I��");
        }


        /// <summary>
        /// �L�����̈ړ������p
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private async UniTask AppendMoveAsync(Sequence sequence, Vector3 newPos) {
           await sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
                     ChangeDirection(chara.placeNo);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// DOTweenList �p(DOTweenList ���̂ɂ� Sequence �@�\�͂Ȃ��̂ŁA�ǉ��������Ԃɏ����������ꍇ�ɂ� Sequence �����O�ŗp�ӂ���)
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private Tween AppendMove(Sequence sequence, Vector3 newPos) {
            return sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                 .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
                     ChangeDirection(chara.placeNo);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// �L�����̂���p�l���̔ԍ����X�V
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
        /// �L�����̌����̍X�V
        /// </summary>
        /// <param name="placeNo"></param>
        private void ChangeDirection(int placeNo) {

        }
    }
}