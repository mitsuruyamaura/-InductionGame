using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

namespace yamap_BoardGame {

    public class DiceRollObserver : MonoBehaviour {

        //public Subject<int> OnDiceRolledObservable = new();
        public ReactiveProperty<int> DiceRoll = new();

        /// <summary>
        /// �_�C�X��U��
        /// </summary>
        /// <param name="max"></param>
        public void RollDice(int max) {
            //this.OnDiceRolledObservable.OnNext(Random.Range(0, max));

            // �����l�ł��w�ǂ���悤�ɂ���
            DiceRoll.SetValueAndForceNotify(Random.Range(0, max + 1));
            //Debug.Log("�_�C�X���[�� �o�� : " + DiceRoll.Value);
        }

        /// <summary>
        /// �Œ�l���_�C�X�ɃZ�b�g
        /// </summary>
        /// <param name="setRoll"></param>
        public void SetDice(int setRoll) {
            // �����l�ł��w�ǂ���悤�ɂ���
            DiceRoll.SetValueAndForceNotify(setRoll);
        }
    }
}