using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private PlayerAnimation playerAnim;
    private EnemyNavigationController navigationController;
    private float downInterval = 2.0f;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }

        if (!TryGetComponent(out navigationController)) {
            Debug.Log("EnemyNavigationController �擾�o���܂���B");
        }
    }


    private void OnTriggerEnter(Collider other) {
        // �X�q�� Rigidbody ���Ȃ��̂ŁAColliderEnter �ł͐ڐG���Ȃ�
        //Debug.Log("Collider Hit");

        if (other.TryGetComponent(out Hat hat)) {

            // TODO
            Debug.Log("�C��");

            StartCoroutine(DownEnemy());
        }
    }

    /// <summary>
    /// �_�E�����N���オ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownEnemy() {
        // �X�e�[�g�؂�ւ��@�d�������h�~
        navigationController.CurrentEnemyState = EnemyState.Down;

        // �ړ���~
        navigationController.StopNavigation();

        // �_�E���A�j���Đ�
        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, true);

        // �N���オ��܂ł̑ҋ@����
        yield return new WaitForSeconds(downInterval);

        // TODO �|���Ă���ꍇ�ɂ͔j��


        // ����ȊO�͂��̏�ɋN���オ��
        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, false);

        // TODO ���邢�́A�����̈ʒu�ɖ߂�


        // ������Ԃɖ߂�
        navigationController.InitEnemy();
    }
}