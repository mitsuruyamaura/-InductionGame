using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource myAudio;

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            myAudio.Play();
        }
    }
}
