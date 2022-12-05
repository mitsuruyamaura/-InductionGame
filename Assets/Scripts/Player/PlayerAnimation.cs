using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー用のキャラクターのアニメの種類
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
            Debug.Log("Animator を取得出来ません");
        }        
    }

    /// <summary>
    /// 移動アニメの再生と停止
    /// </summary>
    /// <param name="speed"></param>
    public void MoveAnimation(float speed) {
        anim.SetFloat(PlayerAnimationState.Speed.ToString(), speed);
    }

    /// <summary>
    /// Bool パラメータのアニメの再生と停止
    /// </summary>
    /// <param name="nextAnimState"></param>
    /// <param name="isChange"></param>

    public void ChangeAnimationBool(PlayerAnimationState nextAnimState, bool isChange) {
        // デバッグ用　Start のタイミングで取得が間に合わないときがあるため、その回避用
        //if (!TryGetComponent(out anim)) {
        //    Debug.Log("Animator を取得出来ません");
        //}
        //Debug.Log(nextAnimState);
        anim.SetBool(nextAnimState.ToString(), isChange);
    }

    /// <summary>
    /// Trigger パラメータのアニメの再生
    /// </summary>
    /// <param name="nextAnimState"></param>
    public void ChangeAnimationFromTrigger(PlayerAnimationState nextAnimState) {
        anim.SetTrigger(nextAnimState.ToString());
    }

    /// <summary>
    /// Animator コンポーネントの取得用
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator() {
        return anim;
    }
}