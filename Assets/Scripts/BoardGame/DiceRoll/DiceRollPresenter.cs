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

            // �{�^���̃C�x���g�w��
            rollDiceDispatcher.DispatchRollButton(maxDice, diceRollObserver);

            // �_�C�X�̍w�ǁB�o�ڂ��ς�邽�тɏo�ڂ̕\���X�V����
            diceRollObserver.DiceRoll
                .Subscribe(value => diceRollViewer.ShowDiceRoll(value))
                .AddTo(this);

            // �X�^�[�g���̏o�ڕ\���X�V
            diceRollViewer.ShowDiceRoll(0);
        }
    }
}