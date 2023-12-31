using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    public class MarkerFactory : MonoBehaviour {
        
        [SerializeField]
        private Marker playerMarkerPrefab;

        [SerializeField]
        private Marker opponentMarkerPrefab;

        [SerializeField]
        private Ease ease;


        /// <summary>
        /// マーカーの作成
        /// </summary>
        /// <param name="ownerType"></param>
        /// <param name="panel"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public Marker GenerateMarker(OwnerType ownerType, Panel panel, int step) {
            // 作成するマーカーの設定
            Marker prefab = ownerType == OwnerType.Player ? playerMarkerPrefab : opponentMarkerPrefab;
            Marker marker = Instantiate(prefab, panel.transform);

            // 高さ調整(2つ目以降が重なるようにする)
            marker.transform.localPosition = new(prefab.transform.position.x, marker.transform.localPosition.y + (step * 0.3f), prefab.transform.position.z);
            float scale = marker.transform.localScale.x;
            marker.transform.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(marker.transform.DOScale(Vector3.one * scale, 1.0f).SetEase(ease));
            //sequence.Join(marker.transform.DOPunchScale(Vector3.one * 1.2f, 1.0f).SetEase(Ease.InQuart));
            return marker;
        }
    }
}