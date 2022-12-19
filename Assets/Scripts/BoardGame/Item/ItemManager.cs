using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    public class ItemManager : MonoBehaviour {

        public static ItemManager instance;

        public List<ItemData> playerItemList = new();
        public List<ItemData> opponentItemList = new();  // ReactiveCollection にしてもいい

        public ItemDataSO itemDataSO;


        void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }


        public void AddItem(OwnerType ownerType, int itemId) {
            if (ownerType == OwnerType.Player) {
                playerItemList.Add(GetItemDataFromId(itemId));
            } else {
                opponentItemList.Add(GetItemDataFromId(itemId));
            }
        }

        public ItemData GetItemDataFromId(int id) {
            return itemDataSO.itemDataList.Find(x => x.id == id);
        }


        public void RemoveItems(OwnerType ownerType, int count) {

            // 最大数を超えないように制限
            if (ownerType == OwnerType.Player) {
                count = Mathf.Min(count, playerItemList.Count);

                for (int i = count - 1; i >= 0; i--) {
                    playerItemList.RemoveAt(i);
                }

                //for (int i = list.Count - 1; i >= 0; i--) {
                //    if (list[i].Taste == Taste.Bad) {
                //        list.RemoveAt(i); // Remove(list[i])は使わない事。
                //    }
                //}
            } else {
                count = Mathf.Min(count, opponentItemList.Count);
                for (int i = count - 1; i >= 0; i--) {
                    opponentItemList.RemoveAt(i);
                }
            }      
        }
    }
}