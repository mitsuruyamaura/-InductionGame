using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ClearManager : MonoBehaviour
{
    private PlayerAnimation playerAnim;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }
        // デバッグ用
        //StartCoroutine(PrepareClearOperationAsync());
    }

    /// <summary>
    /// クリアの準備
    /// </summary>
    public IEnumerator PrepareClearOperationAsync() {
        // クリア視点のカメラをセット
        MainCameraManager.instance.ClearCameraView();

        yield return new WaitForSeconds(1.5f);

        // カメラのターゲット追従を停止
        MainCameraManager.instance.CutFollowTarget();

        // カメラの方を向く
        transform.DOLookAt(MainCameraManager.instance.GetClearCameraTran().position, 1.0f)
            .SetEase(Ease.InQuart)
            .OnComplete(() => 
            {
                // クリアアニメ再生
                playerAnim.ChangeAnimationBool(PlayerAnimationState.Clear, true);
            });
    }
}
