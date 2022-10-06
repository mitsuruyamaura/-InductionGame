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
    private string doorAnimParameter = "Opened";

    /// <summary>
    /// ドアの開閉
    /// </summary>
    /// <param name="playerNavigationController"></param>
    /// <returns></returns>
    public IEnumerator ActivateDoor(PlayerNavigationController playerNavigationController) {

        // カメラ移動
        MainCameraManager.instance.MainCamera.Priority = 5;
        myCamera.Priority = 10;

        // プレイヤーの移動制限
        playerNavigationController.CurrentPlayerState = PlayerState.Wait;

        yield return new WaitForSeconds(2.0f);

        // ドアの開閉状態を切り替え
        isGoalOpen = !isGoalOpen;

        // ドアの開閉アニメ再生
        anim.SetBool(doorAnimParameter, isGoalOpen);

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
