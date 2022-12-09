using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    public class Panel : MonoBehaviour {

        private int no;

        // 次に進む向きの種類　配列で用意。１つしかなければそちらに行く。2つ以上あるときには選択肢を出す
        public DirectionType directionType;

        // イベントの種類の enum


        // 陣地
        public List<Marker> markerList = new();
    }
}