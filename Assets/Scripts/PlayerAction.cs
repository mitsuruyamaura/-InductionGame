using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            HatAction(Input.mousePosition);
        }    
    }

    /// <summary>
    /// 帽子を飛ばす(ギミックを動かしたり、エネミーを攻撃できる)
    /// </summary>
    private void HatAction(Vector3 clickPos) {
        
        // オフメッシュ移動によるジャンプ中は処理しない
        if (navigationController.GetOnOffMeshLink()) {
            return;
        }

        // 移動中以外は処理しない
        if (navigationController.CurrentPlayerState != PlayerState.Play) {
            return;
        }

        // 移動不可にする
        navigationController.CurrentPlayerState = PlayerState.Wait;

        //Debug.Log(clickPos);

        Ray ray = Camera.main.ScreenPointToRay(clickPos);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100)) {

            // 向きをタップした方向に合わせる
            transform.DOLookAt(hit.point, 0.25f).SetEase(Ease.InQuart).SetLink(gameObject);

            // 帽子アクションアニメ再生
            playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Hit);
        }
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
