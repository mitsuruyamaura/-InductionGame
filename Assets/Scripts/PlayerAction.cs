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
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("PlayerNavigationController �擾�o���܂���B");
        }

        // TODO UniRx �Ń{�^������

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HatAction(Input.mousePosition);
        }    
    }

    /// <summary>
    /// �X�q���΂�(�M�~�b�N�𓮂�������A�G�l�~�[���U���ł���)
    /// </summary>
    private void HatAction(Vector3 clickPos) {
        
        // �I�t���b�V���ړ��ɂ��W�����v���͏������Ȃ�
        if (navigationController.GetOnOffMeshLink()) {
            return;
        }

        // �ړ����ȊO�͏������Ȃ�
        if (navigationController.CurrentPlayerState != PlayerState.Play) {
            return;
        }

        // �ړ��s�ɂ���
        navigationController.CurrentPlayerState = PlayerState.Wait;

        //Debug.Log(clickPos);

        Ray ray = Camera.main.ScreenPointToRay(clickPos);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100)) {

            // �������^�b�v���������ɍ��킹��
            transform.DOLookAt(hit.point, 0.25f).SetEase(Ease.InQuart).SetLink(gameObject);

            // �X�q�A�N�V�����A�j���Đ�
            playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Hit);
        }
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
