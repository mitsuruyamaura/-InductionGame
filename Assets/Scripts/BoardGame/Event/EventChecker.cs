using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace yamap_BoardGame {

    public class EventChecker : MonoBehaviour {

        [SerializeField]
        private MarkerFactory markerFactory;


        public async UniTask CheckEventAsync(Charactor chara, Panel panel) {
            if (panel == null) {
                return;
            }

            // 誰も所有していない場合
            if (panel.markerList.Count == 0) {
                panel.markerList.Add(markerFactory.GenerateMarker(chara.ownerType, panel, panel.markerList.Count));
                Debug.Log("新規作成");
                ItemManager.instance.AddItem(chara.ownerType, panel.GetRandomItemIds());
                return;
            } 
                
            // すでに自分が所有している場合
            if (chara.ownerType == panel.markerList[0].ownerType) {
                // 増産
                panel.markerList.Add(markerFactory.GenerateMarker(chara.ownerType, panel, panel.markerList.Count));
                Debug.Log("増産");
                ItemManager.instance.AddItem(chara.ownerType, panel.GetRandomItemIds());
                return;
            }

            // 敵が所有している場合、HP を減らす
            chara.Hp.Value -= panel.markerList.Count;
            Debug.Log(chara.ownerType + " 残りHP : " + chara.Hp.Value);

            // アイテムをランダムで破棄
            ItemManager.instance.RemoveItems(chara.ownerType, panel.markerList.Count);

            // マーカーを破棄
            panel.RemoveMarkers();
            
            await UniTask.Yield();
        }
    }
}