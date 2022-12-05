using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    public class RollDiceButtonDispatcher : MonoBehaviour {

        private Button btnRollDice;

        [SerializeField]
        private DiceRollObserver diceRollObserver;

        [SerializeField]
        private int maxDice = 6;


        void Start() {
            if (TryGetComponent(out Button btnRollDice)) {
                btnRollDice.OnClickAsObservable()
                    .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
                    .Subscribe(_ => diceRollObserver.RollDice(maxDice))
                    .AddTo(this);
            }
        }
    }
}