using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        // �X�q�� Rigidbody ���Ȃ��̂ŁAColliderEnter �ł͐ڐG���Ȃ�
        //Debug.Log("Collider Hit");

        if (other.TryGetComponent(out Hat hat)) {

            // TODO
            Debug.Log("�C��");
        }
    }
}