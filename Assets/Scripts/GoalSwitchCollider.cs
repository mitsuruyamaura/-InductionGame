using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSwitchCollider : GimmickColliderBase
{
    [SerializeField]
    private GoalGimmick goalGimmick;

    private BoxCollider boxCol;

    protected override void Start()
    {
        TryGetComponent(out boxCol);
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerNavigationController playerNavigationController)) {
            boxCol.enabled = false;
            StartCoroutine(goalGimmick.PlayOpenDoor(playerNavigationController));
        }
    }
}
