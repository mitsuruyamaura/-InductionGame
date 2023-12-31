using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace yamap_BoardGame {

    public class CameraObserver : MonoBehaviour {
        public ReactiveProperty<int> Priority;
    }
}