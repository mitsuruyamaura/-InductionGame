using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private string layerName = "Floor";
    //private int layerMask = 1 << 6;　　　// ビット演算でマスクを作成する。未使用
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
            // 移動できる地点か判定する
            CheckRoot();
        }

        // 目的地に近づいたら
        if (Vector3.Distance(transform.position, destination) <= 0.15f) {
            // 停止させる
            //agent.speed = 0;
            playerAnim.MoveAnimation(0);
        }
    }

    /// <summary>
    /// クリックした地点が NavMesh 上の経路か判定
    /// </summary>
    private void CheckRoot() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray.direction);
        //Debug.Log(LayerMask.NameToLayer("Floor"));
        
        // 移動可能な地点であれば
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100, LayerMask.GetMask(layerName))) {
            // NavMesh で移動
            NavigationMove(hit.point);
            // 目的地を登録
            destination = hit.point;
        }
    }

    /// <summary>
    /// NavMesh による経路移動と移動アニメ同期
    /// </summary>
    /// <param name="movePos"></param>
    private void NavigationMove(Vector3 movePos) {
        //Debug.Log(movePos);
        agent.speed = moveSpeed;
        agent.SetDestination(movePos);
        playerAnim.MoveAnimation(agent.speed);
    }
}
