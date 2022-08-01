using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out Hat hat)) {

            // TODO
            Debug.Log("ãCê‚");
        }
    }
}