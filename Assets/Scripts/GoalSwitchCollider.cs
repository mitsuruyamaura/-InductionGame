using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSwitchCollider : GimmickColliderBase
{
    [SerializeField]
    private GoalGimmick goalGimmick;

    [SerializeField]
    private SwitchLever switchLever;

    private BoxCollider boxCol;

    protected override void Start()
    {
        TryGetComponent(out boxCol);
    }

    protected override void OnTriggerEnter(Collider other) {

        // ���łɃ��o�[�𑀍�ς݂ŁA�ēx�̑��삪�ł��Ȃ����o�[�̏ꍇ�A�������Ȃ�
        if (switchLever.IsOnlyOnceActivated && !switchLever.IsAnyTimeSwitch) {
            return;
        }

        if (other.TryGetComponent(out Hat hat)) {    //PlayerNavigationController playerNavigationController
            boxCol.enabled = false;
            switchLever.SwitchActivateLever();
            //StartCoroutine(goalGimmick.PlayOpenDoor(hat.PlayerNavigationController));
            StartCoroutine(PreparePlayOpenDoor(hat.PlayerNavigationController));         
        }
    }


    private IEnumerator PreparePlayOpenDoor(PlayerNavigationController playerNavigationController) {
        yield return StartCoroutine(goalGimmick.PlayOpenDoor(playerNavigationController));

        if (switchLever.IsAnyTimeSwitch) {
            boxCol.enabled = true;
        }
    }
}