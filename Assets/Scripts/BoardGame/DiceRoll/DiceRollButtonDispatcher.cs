using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    [RequireComponent(typeof(Button))]
    public class DiceRollButtonDispatcher : MonoBehaviour {

        /// <summary>
        /// ダイスを振るイベントを駆動
        /// </summary>
        /// <param name="maxDice"></param>
        public Button DispatchRollButton(int maxDice, DiceRollObserver diceRollObserver) {

            if (TryGetComponent(out Button btnRollDice)) {
                // ボタンの購読。ここからダイスを振るイベントを起動する
                btnRollDice.OnClickAsObservable()
                    // TODO HP かステートの確認を入れる
                    .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
                    .Subscribe(_ => diceRollObserver.RollDice(maxDice))
                    .AddTo(this);
                return btnRollDice;
            } else {
                return null;
            }
        }
    }
}