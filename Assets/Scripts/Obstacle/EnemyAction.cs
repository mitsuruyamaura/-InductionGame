using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    [SerializeField]
    private BoxCollider weaponCollider;

    private PlayerAnimation playerAnim;
    private EnemyNavigationController navigationController;

    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("EnemyNavigationController �擾�o���܂���B");
        }
    }

    /// <summary>
    /// �A�j���[�V�����C�x���g�p
    /// </summary>
    /// <param name="switchNo"></param>
    public void SwitchCollider(int switchNo) {
        // �X�q�̃R���C�_�[�̃I���I�t�؂�ւ�
        weaponCollider.enabled = switchNo == 1 ? true : false;
    }
}
