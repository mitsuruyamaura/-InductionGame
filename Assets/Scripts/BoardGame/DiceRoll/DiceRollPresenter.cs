using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace yamap_BoardGame {

    [RequireComponent(typeof(DiceRollObserver))]
    public class DiceRollPresenter : MonoBehaviour {

        [SerializeField]
        private DiceRollButtonDispatcher rollDiceDispatcher;

        [SerializeField]
        private DiceRollViewer diceRollViewer;

        [SerializeField]
        private int maxDice = 6;

        private DiceRollObserver diceRollObserver;


        void Start() {
            TryGetComponent(out diceRollObserver);
            diceRollViewer.InitViewer();

            // ボタンのイベント購読
            rollDiceDispatcher.DispatchRollButton(maxDice, diceRollObserver);

            // ダイスの購読。出目が変わるたびに出目の表示更新する
            diceRollObserver.DiceRoll
                .Subscribe(value => diceRollViewer.ShowDiceRoll(value))
                .AddTo(this);

            // スタート時の出目表示更新
            diceRollViewer.ShowDiceRoll(0);
        }
    }
}