using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCameraManager : MonoBehaviour
{
    public static MainCameraManager instance;

    [SerializeField]
    private CinemachineVirtualCamera mainCamera;

    public CinemachineVirtualCamera MainCamera { get => mainCamera; }


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}