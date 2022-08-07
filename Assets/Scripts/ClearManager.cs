using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ClearManager : MonoBehaviour
{
    private CinemachineVirtualCamera clearCamera;
    private PlayerAnimation playerAnim;

    [SerializeField]
    private Transform clearPos;


    void Start()
    {
        clearCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        if (!clearCamera) {
            Debug.Log("Camera 取得出来ません。");
        }

        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }

        ClearCamera();
    }


    public void ClearCamera() {

        clearCamera.Priority = 20;

        //playerAnim.ChangeAnimationBool(PlayerAnimationState.Clear, true);

        transform.DOLookAt(clearPos.position, 1.0f).SetEase(Ease.InQuart);
    }
}
