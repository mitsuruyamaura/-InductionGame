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
            StartCoroutine(PrepareActivateDoor(hat.PlayerNavigationController));         
        }
    }

    /// <summary>
    /// �h�A�J�̏���
    /// </summary>
    /// <param name="playerNavigationController"></param>
    /// <returns></returns>
    private IEnumerator PrepareActivateDoor(PlayerNavigationController playerNavigationController) {
        // �h�A�̊J�A�j�����I������܂őҋ@
        yield return StartCoroutine(goalGimmick.ActivateDoor(playerNavigationController));

        // �J��Ԃ��N���ł��郌�o�[�̏ꍇ�̂݁A�R���C�_�[���I���ɂ���
        if (switchLever.IsAnyTimeSwitch) {
            boxCol.enabled = true;
        }
    }
}