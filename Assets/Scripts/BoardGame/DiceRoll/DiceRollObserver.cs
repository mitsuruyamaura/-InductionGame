using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

namespace yamap_BoardGame {

    public class DiceRollObserver : MonoBehaviour {

        //public Subject<int> OnDiceRolledObservable = new();
        public ReactiveProperty<int> DiceRoll = new();

        public void RollDice(int max) {
            //this.OnDiceRolledObservable.OnNext(Random.Range(0, max));
            DiceRoll.Value = Random.Range(0, max);
        }


        public void SetDice(int setRoll) {
            DiceRoll.Value = setRoll;
        }
    }
}