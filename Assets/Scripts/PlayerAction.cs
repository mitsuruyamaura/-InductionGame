using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private BoxCollider hatCollider;

    private PlayerAnimation playerAnim;
    private PlayerNavigationController navigationController;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("PlayerNavigationController 取得出来ません。");
        }

        // TODO UniRx でボタン制御

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HatAction();
        }    
    }

    /// <summary>
    /// 帽子を飛ばす(ギミックを動かしたり、エネミーを攻撃できる)
    /// </summary>
    private void HatAction() {
        if (navigationController.CurrentPlayerState != PlayerState.Play) {
            return;
        }
        // 移動可否のオンオフ切り替え
        navigationController.CurrentPlayerState = PlayerState.Wait;

        playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Hit);
    }

    /// <summary>
    /// アニメーションイベント用
    /// </summary>
    /// <param name="switchNo"></param>
    public void SwitchCollider(int switchNo) {
        // 帽子のコライダーのオンオフ切り替え
        hatCollider.enabled = switchNo == 1 ? true : false;

        // 移動可否のオンオフ切り替え
        navigationController.CurrentPlayerState = switchNo == 1 ? PlayerState.Wait : PlayerState.Play;
    }
}
