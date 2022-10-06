using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TNRD;

// MonoBehaviour の参照を Interface でインスペクターにシリアライズする
// https://baba-s.hatenablog.com/entry/2022/07/25/090000

namespace YamaP_Utility {

    public class EntryPoint : MonoBehaviour {

        //[SerializeReference]  // 2019.3 より。インターフェースをインスペクターに表示 -> ただし、シリアライズはスクリプト経由でないとできない
        //public List<IEntryRun> entryList = new();

        // SerializableInterface アセットを使う。Value の中に格納されている
        public List<SerializableInterface<IEntryRun>> entryList = new();


        void Awake() {
            DOTween.Init();

            foreach (var entry in entryList) {
                entry.Value?.EntryRun();   // entry? では null チェックしない
            }
        }
    }
}