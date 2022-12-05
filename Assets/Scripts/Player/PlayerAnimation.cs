using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�p�̃L�����N�^�[�̃A�j���̎��
/// </summary>
public enum PlayerAnimationState {
    Speed,
    Hit,
    Down,
    Clear,
    Idle,
    Jump,

}

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        if (!TryGetComponent(out anim)) {
            Debug.Log("Animator ���擾�o���܂���");
        }        
    }

    /// <summary>
    /// �ړ��A�j���̍Đ��ƒ�~
    /// </summary>
    /// <param name="speed"></param>
    public void MoveAnimation(float speed) {
        anim.SetFloat(PlayerAnimationState.Speed.ToString(), speed);
    }

    /// <summary>
    /// Bool �p�����[�^�̃A�j���̍Đ��ƒ�~
    /// </summary>
    /// <param name="nextAnimState"></param>
    /// <param name="isChange"></param>

    public void ChangeAnimationBool(PlayerAnimationState nextAnimState, bool isChange) {
        // �f�o�b�O�p�@Start �̃^�C�~���O�Ŏ擾���Ԃɍ���Ȃ��Ƃ������邽�߁A���̉��p
        //if (!TryGetComponent(out anim)) {
        //    Debug.Log("Animator ���擾�o���܂���");
        //}
        //Debug.Log(nextAnimState);
        anim.SetBool(nextAnimState.ToString(), isChange);
    }

    /// <summary>
    /// Trigger �p�����[�^�̃A�j���̍Đ�
    /// </summary>
    /// <param name="nextAnimState"></param>
    public void ChangeAnimationFromTrigger(PlayerAnimationState nextAnimState) {
        anim.SetTrigger(nextAnimState.ToString());
    }

    /// <summary>
    /// Animator �R���|�[�l���g�̎擾�p
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator() {
        return anim;
    }
}