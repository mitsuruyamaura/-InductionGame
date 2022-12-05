using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    public class DiceRenderer : MonoBehaviour {

        [SerializeField]
        private DiceRollObserver diceRollObserver;

        private Text txtDiceRoll;


        void Start() {
            if (TryGetComponent(out txtDiceRoll)) {

                diceRollObserver.DiceRoll
                    .Subscribe(value => ShowDiceRoll(value + 1))
                    .AddTo(this);
            }
            ShowDiceRoll(0);
        }

        private void ShowDiceRoll(int value) {
            txtDiceRoll.text = value.ToString();
        }
    }
}