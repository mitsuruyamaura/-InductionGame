using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera[] virtualCameras;

    [SerializeField]
    CinemachineVirtualCamera topViewCamera;


    /// <summary>
    /// ƒLƒƒƒ‰ƒJƒƒ‰‚ÌØ‚è‘Ö‚¦
    /// </summary>
    /// <param name="index"></param>
    public void SwitchCharaCamera(int index) {

        for (int i = 0; i < virtualCameras.Length; i++) {
            virtualCameras[i].Priority = index == i ? 10 : 5;
        }
    }
}
