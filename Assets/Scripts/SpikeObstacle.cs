using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class SpikeObstacle : ObstacleBase
{
    [SerializeField]
    private Ease ease;

    protected override void OnEnterObstacle() {
        base.OnEnterObstacle();

        Sequence sequence = DOTween.Sequence(); 
        sequence.AppendInterval(1.5f);
        sequence.Append(transform.DOLocalMoveY(-1.0f, 1.5f).SetRelative().SetEase(ease).SetLink(gameObject)); ;
        sequence.AppendInterval(1.5f);
        sequence.Append(transform.DOLocalMoveY(1.0f, 1.5f).SetRelative().SetEase(ease)).SetLoops(-1, LoopType.Restart);
    }
}