using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace yamap_BoardGame {

    [RequireComponent(typeof(Chara))]
    public class CharactorRenderer : MonoBehaviour {

        [SerializeField]
        private DiceRollObserver diceRollObserver;

        [SerializeField]
        private Field field;

        private Chara chara;


        void Start() {
            TryGetComponent(out chara);
            diceRollObserver.DiceRoll.Subscribe(value => Move(value)).AddTo(this);
        }


        private void Move(int moeConut) {

            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i <= moeConut; i++) {

                int nextPlace = i + chara.placeNo;
                if (nextPlace >= field.panels.Length) {
                    nextPlace -= field.panels.Length;
                }

                Vector3 nextPos = field.panels[nextPlace].transform.localPosition;
                AppendMove(sequence, nextPos);
            }
        }


        /// <summary>
        /// ÉLÉÉÉâÇÃà⁄ìÆèàóùóp
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="newPos"></param>
        private void AppendMove(Sequence sequence, Vector3 newPos) {
            sequence.Append(transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InQuart))
                .OnComplete(() => {
                    chara.placeNo = GetNewPlaceNo(chara.placeNo);
                    ChangeDirection(chara.placeNo);
                });
        }


        private int GetNewPlaceNo(int placeNo) {
            int newPlaceNo = placeNo;

            if (field.panels.Length - 1 != placeNo) {
                newPlaceNo++;
            } else {
                newPlaceNo = 0;
            }
            return newPlaceNo;
        }


        private void ChangeDirection(int placeNo) {

        }
    }
}