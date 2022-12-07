using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    [RequireComponent(typeof(Text))]
    public class DiceRollViewer : MonoBehaviour {

        private Text txtDiceRoll;


        /// <summary>
        /// 初期設定
        /// </summary>
        public void InitViewer() {
            TryGetComponent(out txtDiceRoll);
        }

        /// <summary>
        /// ダイスの出目の表示更新
        /// </summary>
        /// <param name="value"></param>
        public void ShowDiceRoll(int value) {
            txtDiceRoll.text = value.ToString();
        }
    }
}