using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickColliderBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject gimmickTarget;


    protected virtual void Start() {
        if (gimmickTarget != null) {
            gimmickTarget.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerCollider playerCollider)) {

            // TODO �A�j��������
            gimmickTarget.SetActive(true);
        }
    }
}