using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace yamap_BoardGame {

    [RequireComponent(typeof(Text))]
    public class DiceRollViewer : MonoBehaviour {

        private Text txtDiceRoll;


        /// <summary>
        /// �����ݒ�
        /// </summary>
        public void InitViewer() {
            TryGetComponent(out txtDiceRoll);
        }

        /// <summary>
        /// �_�C�X�̏o�ڂ̕\���X�V
        /// </summary>
        /// <param name="value"></param>
        public void ShowDiceRoll(int value) {
            txtDiceRoll.text = value.ToString();
        }
    }
}