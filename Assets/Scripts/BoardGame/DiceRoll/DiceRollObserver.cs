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
        /// ダイスを振る
        /// </summary>
        /// <param name="max"></param>
        public void RollDice(int max) {
            //this.OnDiceRolledObservable.OnNext(Random.Range(0, max));

            // 同じ値でも購読するようにする
            DiceRoll.SetValueAndForceNotify(Random.Range(0, max + 1));
            //Debug.Log("ダイスロール 出目 : " + DiceRoll.Value);
        }

        /// <summary>
        /// 固定値をダイスにセット
        /// </summary>
        /// <param name="setRoll"></param>
        public void SetDice(int setRoll) {
            // 同じ値でも購読するようにする
            DiceRoll.SetValueAndForceNotify(setRoll);
        }
    }
}