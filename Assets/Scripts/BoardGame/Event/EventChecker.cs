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

            // �N�����L���Ă��Ȃ��ꍇ
            if (panel.markerList.Count == 0) {
                panel.markerList.Add(markerFactory.GenerateMarker(chara.ownerType, panel, panel.markerList.Count));
                Debug.Log("�V�K�쐬");
                ItemManager.instance.AddItem(chara.ownerType, panel.GetRandomItemIds());
                return;
            } 
                
            // ���łɎ��������L���Ă���ꍇ
            if (chara.ownerType == panel.markerList[0].ownerType) {
                // ���Y
                panel.markerList.Add(markerFactory.GenerateMarker(chara.ownerType, panel, panel.markerList.Count));
                Debug.Log("���Y");
                ItemManager.instance.AddItem(chara.ownerType, panel.GetRandomItemIds());
                return;
            }

            // �G�����L���Ă���ꍇ�AHP �����炷
            chara.Hp.Value -= panel.markerList.Count;
            Debug.Log(chara.ownerType + " �c��HP : " + chara.Hp.Value);

            // �A�C�e���������_���Ŕj��
            ItemManager.instance.RemoveItems(chara.ownerType, panel.markerList.Count);

            // �}�[�J�[��j��
            panel.RemoveMarkers();
            
            await UniTask.Yield();
        }
    }
}