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

    [RequireComponent(typeof(Charactor))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class CharactorMover : MonoBehaviour {

        // �C���X�^���V�G�C�g�����ꍇ�ɂ́A���\�b�h�𗘗p���ĊO��������炤

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
        /// �L�����̈ړ�
        /// </summary>
        /// <param name="moveConut"></param>
        /// <returns></returns>
        private async UniTask MoveAsync(int moveConut) {   // 0 �̏o�ڂ�����B���̏ꍇ�͈ړ����Ȃ�

            Debug.Log(moveConut);
            if (moveConut == 0) {
                return;
            }

            // TODO ����p�l���ɂ���ꍇ�ɂ́A�ŏ��Ɉړ����������I������@await ���邱��


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
            // List �̏��������ׂďI������܂őҋ@�BSequence �ɂ��Ă���̂ŁA���Ԃɏ��������
            await dotweenList.PlayForward();

            //await sequence.AppendInterval((moveConut - 1) * 0.3f)
            //    .SetLink(gameObject);
                //.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, this.GetCancellationTokenOnDestroy());
            
            playerAnimation.MoveAnimation(0);

            // �{�^�����ēx������悤�ɂ���

            Debug.Log("�ړ��I��");
        }

        /// <summary>
        /// �L�����̈ړ������p�B���g�p
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private async UniTask AppendMoveAsync(Sequence sequence, Vector3 newPos, DirectionType newDirectionType) {
           await sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
                     ChangeDirection(newDirectionType);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// DOTweenList �p(DOTweenList ���̂ɂ� Sequence �@�\�͂Ȃ��̂ŁA�ǉ��������Ԃɏ����������ꍇ�ɂ� Sequence �����O�ŗp�ӂ���)
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private Tween AppendMove(Sequence sequence, Vector3 newPos, DirectionType newDirectionType) {
            return sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                 .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo);
                     Debug.Log(chara.placeNo);

                     // �L�����̌�����i�s�ɍ��킹��
                     ChangeDirection(newDirectionType);

                     // TODO �C�x���g

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
        private void ChangeDirection(DirectionType nextDirectionType) {
            if (nextDirectionType == DirectionType.None) {
                return;
            }
            transform.DORotate(new(transform.position.x, GetRotateAngle(nextDirectionType), transform.position.z), 0.5f).SetEase(Ease.Linear);
        }

        /// <summary>
        /// �L�����̌�����i�s�����ɍ��킹��
        /// </summary>
        /// <param name="nextDirectionType"></param>
        /// <returns></returns>
        private float GetRotateAngle(DirectionType nextDirectionType) {
            // ���f���̏����A���O���ɂ���Ēl��ύX���邱�ƁB�����ł� 90 �̊p�x�Ő��ʕ�����i�ނ悤�ɂ��Ă���P�[�X
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