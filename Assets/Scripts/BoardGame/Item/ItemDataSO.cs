using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "Create ItemDataSO")]
    public class ItemDataSO : ScriptableObject {
        public List<ItemData> itemDataList = new();
    }
}