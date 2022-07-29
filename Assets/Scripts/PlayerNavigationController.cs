using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// プレイヤーの状態の種類
/// </summary>
public enum PlayerState {
    Down,
    GameUp,
    Wait,
    Play,
}

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

    [SerializeField]
    private PlayerState currentPlayerState;

    public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
        set => currentPlayerState = value;
    }


    public bool GetOnOffMeshLink() {
        return agent.isOnOffMeshLink;
    }

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
        CurrentPlayerState = PlayerState.Play;

        StartCoroutine(ObserveOffMeshLink());
    }

    void Update() {
        if (currentPlayerState != PlayerState.Play) {
            agent.speed = 0;
            playerAnim.MoveAnimation(0);
            agent.ResetPath();
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            // 移動できる地点か判定する
            CheckRoot();
        }

        // 目的地に近づいたら
        if (Vector3.Distance(transform.position, destination) <= 0.15f) {
            // 停止させる
            //agent.speed = 0;
            playerAnim.MoveAnimation(0);
            agent.ResetPath();
        }
    }

    /// <summary>
    /// OffMeshLink の監視
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveOffMeshLink() {

        //// OffMeshLink の挙動がオートの場合にはこの処理は行わない
        //if (agent.autoTraverseOffMeshLink) {
        //    yield break;
        //}

        while (true) {
            while (agent.isOnOffMeshLink) {
                if (!playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                    // Trigger だと上手く遷移できないため、Bool に変更
                    //playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Jump);

                    playerAnim.ChangeAnimationBool(PlayerAnimationState.Jump, true);
                }              
                yield return null;
            }
            if (playerAnim.GetAnimator() != null && playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                playerAnim.ChangeAnimationBool(PlayerAnimationState.Jump, false);
                //Debug.Log("OffMeshによる移動終了");
            }


            // NavMesh による移動を停止(Path 情報は保持している)
            //agent.isStopped = true;

            //// TODO 高さがある場合には調整を入れる

            // テレポートする
            //yield return OffMeshLinkProcess(agent.currentOffMeshLinkData.endPos);

            // OffMeshLink の計算終了
            //agent.CompleteOffMeshLink();

            // NavMesh による移動再開
            //agent.isStopped = false;

            yield return null;
        }

        //IEnumerator OffMeshLinkProcess(Vector3 targetPos) {
        //    yield return null;
        //}
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

            // 障害物がある場合には移動させない
            if (hit.collider.TryGetComponent(out ObstacleBase obstacleBase)) {
                return;
            }

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
