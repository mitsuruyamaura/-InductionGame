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
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("PlayerNavigationController �擾�o���܂���B");
        }

        // TODO UniRx �Ń{�^������

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HatAction();
        }    
    }

    /// <summary>
    /// �X�q���΂�(�M�~�b�N�𓮂�������A�G�l�~�[���U���ł���)
    /// </summary>
    private void HatAction() {
        if (navigationController.CurrentPlayerState != PlayerState.Play) {
            return;
        }
        // �ړ��ۂ̃I���I�t�؂�ւ�
        navigationController.CurrentPlayerState = PlayerState.Wait;

        playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Hit);
    }

    /// <summary>
    /// �A�j���[�V�����C�x���g�p
    /// </summary>
    /// <param name="switchNo"></param>
    public void SwitchCollider(int switchNo) {
        // �X�q�̃R���C�_�[�̃I���I�t�؂�ւ�
        hatCollider.enabled = switchNo == 1 ? true : false;

        // �ړ��ۂ̃I���I�t�؂�ւ�
        navigationController.CurrentPlayerState = switchNo == 1 ? PlayerState.Wait : PlayerState.Play;
    }
}
