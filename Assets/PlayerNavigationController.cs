using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private string layerName = "Floor";
    private int layerMask = 1 << 6;
    private PlayerAnimation playerAnim;
    private Vector3 destination;


    [SerializeField]
    private float moveSpeed;


    void Start() {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation を取得出来ません。");
        }
        if (!TryGetComponent(out agent)) {
            Debug.Log("NavMeshAgent を取得出来ません。");
            return;
        }
        //agent.speed = 0;

        //agent.SetDestination(new(1 , 0, 1));
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            CheckRoot();
        }

        if (Vector3.Distance(transform.position, destination) <= 0.5f) {
            agent.speed = 0;
            playerAnim.MoveAnimation(agent.speed);
        }
    }


    private void CheckRoot() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.direction);
        Debug.Log(LayerMask.NameToLayer("Floor"));
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100, LayerMask.GetMask(layerName))) {
            NavigationMove(hit.point);
            destination = hit.point;
        }
    }


    private void NavigationMove(Vector3 movePos) {
        Debug.Log(movePos);
        agent.speed = moveSpeed;
        agent.SetDestination(movePos);
        playerAnim.MoveAnimation(agent.speed);
    }
}
