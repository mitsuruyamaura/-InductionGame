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
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("PlayerNavigationController �擾�o���܂���B");
        }
        if (!TryGetComponent(out capsuleCol)) {
            Debug.Log("CapsuleCollider �擾�o���܂���B");
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
    /// ��Q���ɉ��������A�N�V�����̌���
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

        // �N���オ��܂őҋ@
        yield return new WaitForSeconds(1.0f);

        // �^�b�v��t�J�n
        navigationController.CurrentPlayerState = PlayerState.Play;
    }
}