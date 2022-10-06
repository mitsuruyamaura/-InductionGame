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

    private bool isGoalOpen;   // true �Ȃ�J���Ă���
    private string doorAnimParameter = "Opened";

    /// <summary>
    /// �h�A�̊J��
    /// </summary>
    /// <param name="playerNavigationController"></param>
    /// <returns></returns>
    public IEnumerator ActivateDoor(PlayerNavigationController playerNavigationController) {

        // �J�����ړ�
        MainCameraManager.instance.MainCamera.Priority = 5;
        myCamera.Priority = 10;

        // �v���C���[�̈ړ�����
        playerNavigationController.CurrentPlayerState = PlayerState.Wait;

        yield return new WaitForSeconds(2.0f);

        // �h�A�̊J��Ԃ�؂�ւ�
        isGoalOpen = !isGoalOpen;

        // �h�A�̊J�A�j���Đ�
        anim.SetBool(doorAnimParameter, isGoalOpen);

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
