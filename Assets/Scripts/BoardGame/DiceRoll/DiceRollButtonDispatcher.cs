using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    [RequireComponent(typeof(Button))]
    public class DiceRollButtonDispatcher : MonoBehaviour {

        /// <summary>
        /// �_�C�X��U��C�x���g���쓮
        /// </summary>
        /// <param name="maxDice"></param>
        public void DispatchRollButton(int maxDice, DiceRollObserver diceRollObserver) {

            if (TryGetComponent(out Button btnRollDice)) {
                // �{�^���̍w�ǁB��������_�C�X��U��C�x���g���N������
                btnRollDice.OnClickAsObservable()
                    .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
                    .Subscribe(_ => diceRollObserver.RollDice(maxDice))
                    .AddTo(this);
            }            
        }
    }
}