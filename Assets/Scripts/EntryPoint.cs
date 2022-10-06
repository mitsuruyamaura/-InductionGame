using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TNRD;

// MonoBehaviour �̎Q�Ƃ� Interface �ŃC���X�y�N�^�[�ɃV���A���C�Y����
// https://baba-s.hatenablog.com/entry/2022/07/25/090000

namespace YamaP_Utility {

    public class EntryPoint : MonoBehaviour {

        //[SerializeReference]  // 2019.3 ���B�C���^�[�t�F�[�X���C���X�y�N�^�[�ɕ\�� -> �������A�V���A���C�Y�̓X�N���v�g�o�R�łȂ��Ƃł��Ȃ�
        //public List<IEntryRun> entryList = new();

        // SerializableInterface �A�Z�b�g���g���BValue �̒��Ɋi�[����Ă���
        public List<SerializableInterface<IEntryRun>> entryList = new();


        void Awake() {
            DOTween.Init();

            foreach (var entry in entryList) {
                entry.Value?.EntryRun();   // entry? �ł� null �`�F�b�N���Ȃ�
            }
        }
    }
}