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
        // –Xq‚É Rigidbody ‚ª‚È‚¢‚Ì‚ÅAColliderEnter ‚Å‚ÍÚG‚µ‚È‚¢
        //Debug.Log("Collider Hit");

        if (other.TryGetComponent(out Hat hat)) {

            // TODO
            Debug.Log("‹Câ");
        }
    }
}