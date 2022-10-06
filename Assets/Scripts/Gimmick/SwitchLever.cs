using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLever : MonoBehaviour
{
    private Animator anim;
    private bool isLeverActivated;
    private bool isOnlyOnceActivated;                // ��x���o�[�𑀍삵���� true �ɂȂ�
    private string switchAnimParameter = "Switched";

    public bool IsOnlyOnceActivated { get => isOnlyOnceActivated; }

    [SerializeField]
    private bool isAnyTimeSwitch;           // true �̏ꍇ�A���x�ł��؂�ւ��\

    public bool IsAnyTimeSwitch { get => isAnyTimeSwitch; }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// ���o�[�؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateLever() {

        // ��x��������ł��Ȃ��ꍇ
        if(isOnlyOnceActivated && !isAnyTimeSwitch) {
            return;
        }
        isOnlyOnceActivated = true;
        isLeverActivated = !isLeverActivated;
        anim.SetBool(switchAnimParameter, isLeverActivated);
        
        //Debug.Log(isLeverActivated);
    }
}