using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yamap_BoardGame {

    public class Panel : MonoBehaviour {

        private int no;

        // ���ɐi�ތ����̎�ށ@�z��ŗp�ӁB�P�����Ȃ���΂�����ɍs���B2�ȏ゠��Ƃ��ɂ͑I�������o��
        public DirectionType directionType;

        // �C�x���g�̎�ނ� enum


        // �w�n
        public List<Marker> markerList = new();
    }
}