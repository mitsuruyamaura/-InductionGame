using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider : MonoBehaviour
{
    private PlayerAnimation playerAnim;
    private PlayerNavigationController navigationController;
    private CapsuleCollider capsuleCol;

    [SerializeField]
    private Transform startTran;

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("PlayerNavigationController 取得出来ません。");
        }
        if (!TryGetComponent(out capsuleCol)) {
            Debug.Log("CapsuleCollider 取得出来ません。");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (navigationController.CurrentPlayerState != PlayerState.Play) {
            return;
        }

        if (other.TryGetComponent(out ObstacleBase obstacleBase)) {
            capsuleCol.enabled = false;
            UnityAction action = GetReacion(obstacleBase.ObstacleType);
            action.Invoke();
        }
    }

    /// <summary>
    /// 障害物に応じたリアクションの決定
    /// </summary>
    /// <param name="obstacleType"></param>
    /// <returns></returns>
    private UnityAction GetReacion(ObstacleType obstacleType) {
        return obstacleType switch {
            ObstacleType.Pit => DownPlayer,
            _ => DownPlayer,
        };
    }


    private void DownPlayer() {
        navigationController.CurrentPlayerState = PlayerState.Down;
        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, true);
        StartCoroutine(Restart());
    }

    private IEnumerator Restart() {
        yield return new WaitForSeconds(2.0f);

        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, false);
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);

        transform.position = startTran.position;
        transform.rotation = startTran.rotation;

        meshRenderer.enabled = true;
        capsuleCol.enabled = true;

        // 起き上がるまで待機
        yield return new WaitForSeconds(1.0f);

        // タップ受付開始
        navigationController.CurrentPlayerState = PlayerState.Play;
    }
}