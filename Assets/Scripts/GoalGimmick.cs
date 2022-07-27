using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GoalGimmick : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera myCamera;

    [SerializeField]
    private GameObject switchObj;

    [SerializeField]
    private Animator anim;


    public IEnumerator PlayOpenDoor(PlayerNavigationController playerNavigationController) {

        // �J�����ړ�
        MainCameraManager.instance.MainCamera.Priority = 5;
        myCamera.Priority = 10;

        // �v���C���[�̈ړ�����
        playerNavigationController.CurrentPlayerState = PlayerState.Wait;

        yield return new WaitForSeconds(2.0f);

        // �A�j���Đ�
        anim.SetBool("Opened", true);

        yield return new WaitForSeconds(2.0f);

        // �J�����߂�
        myCamera.Priority = 5;
        MainCameraManager.instance.MainCamera.Priority = 10;

        // �J�������߂�I���܂őҋ@
        yield return new WaitForSeconds(1.0f);

        // �v���C���[�̈ړ���������
        playerNavigationController.CurrentPlayerState = PlayerState.Play;

    }

}
