using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState {
    Speed,
    Action,
    Down,
    Clear,
}

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        if (!TryGetComponent(out anim)) {
            Debug.Log("Animator ���擾�o���܂���");
        }        
    }

    public void MoveAnimation(float speed) {
        anim.SetFloat(PlayerAnimationState.Speed.ToString(), speed);
    }
}