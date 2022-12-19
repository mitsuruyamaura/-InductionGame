using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    public class ItemManager : MonoBehaviour {

        public static ItemManager instance;

        public List<ItemData> playerItemList = new();
        public List<ItemData> opponentItemList = new();  // ReactiveCollection ‚É‚µ‚Ä‚à‚¢‚¢

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

            // Å‘å”‚ğ’´‚¦‚È‚¢‚æ‚¤‚É§ŒÀ
            if (ownerType == OwnerType.Player) {
                count = Mathf.Max(0, playerItemList.Count);

                for (int i = 0; i < count; i++) {
                    playerItemList.Remove(playerItemList[i]);
                }
            } else {
                count = Mathf.Max(0, opponentItemList.Count);
                for (int i = 0; i < count; i++) {
                    opponentItemList.Remove(opponentItemList[i]);
                }
            }      
        }
    }
}