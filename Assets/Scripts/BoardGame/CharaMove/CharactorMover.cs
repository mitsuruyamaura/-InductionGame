using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;


// DOTweenList
// https://blog.gigacreation.jp/entry/2019/11/27/203924

// �yDOTween/UniTask�zForget�������Destroy����ƌx�����o��
// https://takap-tech.com/entry/2022/07/21/020225

namespace yamap_BoardGame {

    [RequireComponent(typeof(Charactor))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class CharactorMover : MonoBehaviour {

        // �C���X�^���V�G�C�g�����ꍇ�ɂ́A���\�b�h�𗘗p���ĊO��������炤

        //[SerializeField]
        //private DiceRollObserver diceRollObserver;

        //[SerializeField]
        //private Field field;

        private PlayerAnimation playerAnimation;
        private Charactor chara;    // Player �̂Ƃ��� Opponent �̂Ƃ��̓A�T�C�����t�ɂ���
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

            //// �G�̏ꍇ
            //if (chara.ownerType == OwnerType.Opponent) {
            //    opponent.IsMyTurn.Where(x => x == true)
            //        .Subscribe(_ => diceRollObserver.RollDice(6))
            //        .AddTo(this);
            //}
        }

        /// <summary>
        /// �L�����̈ړ�
        /// </summary>
        /// <param name="moveConut"></param>
        /// <returns></returns>
        public async UniTask<Panel> MoveAsync(int moveConut, CancellationToken token, EventChecker eventChecker, DiceRollObserver diceRollObserver, Field field) {   // 0 �̏o�ڂ�����B���̏ꍇ�͈ړ����Ȃ�

            Debug.Log(moveConut);
            if (moveConut == 0) {
                // �����̃p�l���̏ꍇ�ɂ͑��Y�ł���悤�ɂ���
                return field.panels[chara.placeNo];
            }

            //// �����̃^�[���I��
            //chara.IsMyTurn.Value = false;


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

                dotweenList.Add(AppendMove(sequence, nextPos, field.panels[nextPlace].directionType, field.panels.Length));

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

            //// �C�x���g����(�}�[�J�[�쐬�� Hp ����)
            //Marker marker = await eventChecker.CheckEventAsync(chara, field.panels[chara.placeNo]);

            //if (marker != null) {               
            //    field.panels[chara.placeNo].markerList.Add(marker);
            //}

            //await UniTask.Delay(1500, cancellationToken: token);


            //opponent.IsMyTurn.Value = true;


            Debug.Log("�ړ��I��");

            return field.panels[chara.placeNo];

            // �G�̃^�[���̏I���܂őҋ@
            //await UniTask.WaitUntil(() => opponent.IsMyTurn.Value == false, cancellationToken: token);

            // �{�^�����ēx������悤�ɂ���


            //Debug.Log("�v���C���[�̃^�[���J�n");
        }

        /// <summary>
        /// �L�����̈ړ������p�B���g�p
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private async UniTask AppendMoveAsync(Sequence sequence, Vector3 newPos, DirectionType newDirectionType, int fieldCount) {
           await sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo, fieldCount);
                     ChangeDirection(newDirectionType);
                     Debug.Log(chara.placeNo);
                 }));
        }

        /// <summary>
        /// DOTweenList �p(DOTweenList ���̂ɂ� Sequence �@�\�͂Ȃ��̂ŁA�ǉ��������Ԃɏ����������ꍇ�ɂ� Sequence �����O�ŗp�ӂ���)
        /// </summary>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private Tween AppendMove(Sequence sequence, Vector3 newPos, DirectionType newDirectionType, int fieldCount) {
            return sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart)  // ���@�����ł���� ) ���ĕ��Ȃ����ƁB���̏ꍇ�AOnComplete ���Ō�ɂ����Ă΂�Ȃ��Ȃ�
                 .OnComplete(() => {
                     chara.placeNo = GetNewPlaceNo(chara.placeNo, fieldCount);
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