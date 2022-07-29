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

    private bool isGoalOpen;   // true なら開いている


    public IEnumerator PlayOpenDoor(PlayerNavigationController playerNavigationController) {

        // カメラ移動
        MainCameraManager.instance.MainCamera.Priority = 5;
        myCamera.Priority = 10;

        // プレイヤーの移動制限
        playerNavigationController.CurrentPlayerState = PlayerState.Wait;

        yield return new WaitForSeconds(2.0f);

        isGoalOpen = !isGoalOpen;

        // アニメ再生
        anim.SetBool("Opened", isGoalOpen);

        yield return new WaitForSeconds(2.0f);

        // カメラ戻す
        myCamera.Priority = 5;
        MainCameraManager.instance.MainCamera.Priority = 10;

        // カメラが戻り終わるまで待機
        yield return new WaitForSeconds(1.0f);

        // プレイヤーの移動制限解除
        playerNavigationController.CurrentPlayerState = PlayerState.Play;
    }
}
