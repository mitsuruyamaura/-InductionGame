using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemyNavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerNavigationController player;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform startTran;


    void Start()
    {
        if (!TryGetComponent(out agent)) {
            Debug.Log("NavMeshAgent 取得出来ません。");
        }   
    }

    void Update()
    {
        if (!agent) {
            return;
        }
        if (!player) {
            return;
        }
        agent.SetDestination(player.transform.position);
    }


    private void OnTriggerEnter(Collider other) {
        if (player) {
            return;
        }

        if (other.TryGetComponent(out player)) {
            agent.speed = moveSpeed;
            agent.SetDestination(player.transform.position);
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out player)) {

            player = null;

            // TODO 初期位置の設定がある場合には、そこに戻す
            if (!startTran) {
                agent.speed = 0;
                agent.ResetPath();
                return;
            }
            agent.SetDestination(startTran.position);
        }
    }
}