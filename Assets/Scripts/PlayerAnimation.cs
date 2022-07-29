using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState {
    Speed,
    Hit,
    Down,
    Clear,
    Idle,
    Jump,

}

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        if (!TryGetComponent(out anim)) {
            Debug.Log("Animator ÇéÊìæèoóàÇ‹ÇπÇÒ");
        }        
    }

    public void MoveAnimation(float speed) {
        anim.SetFloat(PlayerAnimationState.Speed.ToString(), speed);
    }


    public void ChangeAnimationBool(PlayerAnimationState nextAnimState, bool isChange) {
        anim.SetBool(nextAnimState.ToString(), isChange);
    }


    public void ChangeAnimationFromTrigger(PlayerAnimationState nextAnimState) {
        anim.SetTrigger(nextAnimState.ToString());
    }


    public Animator GetAnimator() {
        return anim;
    }
}