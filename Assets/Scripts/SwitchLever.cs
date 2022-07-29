using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLever : MonoBehaviour
{
    private Animator anim;
    private bool isLeverActivated;
    private bool isOnlyOnceActivated;                // 一度レバーを操作したら true になる
    private string switchAnimParameter = "Switched";

    public bool IsOnlyOnceActivated { get => isOnlyOnceActivated; }

    [SerializeField]
    private bool isAnyTimeSwitch;           // true の場合、何度でも切り替え可能

    public bool IsAnyTimeSwitch { get => isAnyTimeSwitch; }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// レバー切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateLever() {

        // 一度しか操作できない場合
        if(isOnlyOnceActivated && !isAnyTimeSwitch) {
            return;
        }
        isOnlyOnceActivated = true;
        isLeverActivated = !isLeverActivated;
        anim.SetBool(switchAnimParameter, isLeverActivated);
        
        //Debug.Log(isLeverActivated);
    }
}