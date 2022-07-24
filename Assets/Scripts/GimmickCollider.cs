using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject gimmickTarget;


    private void Start() {
        if (gimmickTarget != null) {
            gimmickTarget.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerCollider playerCollider)) {

            // TODO ÉAÉjÉÅÇ≥ÇπÇÈ
            gimmickTarget.SetActive(true);
        }
    }
}
