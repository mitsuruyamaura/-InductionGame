using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    string layerName = "Floor";
    int layerMask = 1 << 6;

    [SerializeField]
    private float moveSpeed;

    void Start() {
        if (!TryGetComponent(out agent)) {
            Debug.Log("NavMeshAgent ‚ðŽæ“¾o—ˆ‚Ü‚¹‚ñB");
            return;
        }
        //agent.speed = 0;

        //agent.SetDestination(new(1 , 0, 1));
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            CheckRoot();
        }
    }


    private void CheckRoot() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.direction);
        Debug.Log(LayerMask.NameToLayer("Floor"));
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100, LayerMask.GetMask(layerName))) {
            NavigationMove(hit.point);
        }
    }


    private void NavigationMove(Vector3 movePos) {
        Debug.Log(movePos);
        agent.speed = moveSpeed;
        agent.SetDestination(movePos);

    }
}
