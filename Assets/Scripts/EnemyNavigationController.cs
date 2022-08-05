using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/// <summary>
/// エネミーの状態
/// </summary>
public enum EnemyState {
    Move,
    Idle,
    Attack,
    Down
}

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemyNavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerNavigationController player;
    private PlayerAnimation playerAnim;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform startTran;

    [SerializeField]
    private float attackInterval;

    private float timer;
    private float lookAroundInterval = 3.5f;

    private EnemyState currentEnemyState = EnemyState.Idle;

    /// <summary>
    /// 
    /// </summary>
    public EnemyState CurrentEnemyState
    {
        get => currentEnemyState;
        set => currentEnemyState = value;
    }


    void Start()
    {
        if (!TryGetComponent(out agent)) {
            Debug.Log("NavMeshAgent 取得出来ません。");
        }
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }
        // 初期化
        InitEnemy();
    }

    void Update()
    {
        if (!agent) {
            return;
        }

        // アイドル中は一定時間ごとに見回りする
        if (CurrentEnemyState == EnemyState.Idle) {
            LookAround();
            return;
        }

        if (!player) {
            return;
        }

        // 一定距離に近づいたら
        if (player && Vector3.Distance(transform.position, player.transform.position) <= 2.0f) {
            CurrentEnemyState = EnemyState.Attack;
            StartCoroutine(ObserveAttack());
        }

        // ダウン中か攻撃中の時は処理しない
        if (CurrentEnemyState == EnemyState.Down || CurrentEnemyState == EnemyState.Attack) {
            return;
        }

        // それ以外(Move)は移動
        agent.SetDestination(player.transform.position);
    }

    /// <summary>
    /// 攻撃の監視
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveAttack() {
        float timer = 0;
        while(CurrentEnemyState == EnemyState.Attack) {
            timer += Time.deltaTime;
            if (timer >= attackInterval) {
                timer = 0;
                playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Hit);
            }
            yield return null;
        }
        Debug.Log("攻撃終了");        
    }

    private void OnTriggerEnter(Collider other) {
        // ダウン中か攻撃中の時は処理しない
        if (CurrentEnemyState == EnemyState.Down || CurrentEnemyState == EnemyState.Attack) {
            //Debug.Log(CurrentEnemyState);
            return;
        }

        // すでにプレイヤーを見つけている場合には処理しない
        if (player) {
            //Debug.Log(player);
            return;
        }

        // 侵入したコライダーを持つゲームオブジェクトがプレイヤーであるか判定
        if (other.TryGetComponent(out player)) {
            Debug.Log("プレイヤー発見");

            // プレイヤーの場合には、プレイヤーの位置を NavMeshAgent の目標地点としてセット
            SetNavigation(player.transform.position);
        }
    }

    private void OnTriggerExit(Collider other) {

        // トリガーの範囲からいなくなったコライダーを持つゲームオブジェクトがプレイヤーであるか判定
        if (other.TryGetComponent(out player)) {
            Debug.Log("プレイヤー見失う");
            StopNavigation();

            // 初期位置の設定がない場合には、その場にいる
            if (!startTran) {              
                return;
            }
            // 初期位置の設定がある場合には、そこに戻す
            agent.SetDestination(startTran.position);
        }
    }

    /// <summary>
    /// NavMesh による経路移動と移動アニメ同期
    /// </summary>
    /// <param name="movePos"></param>
    private void SetNavigation(Vector3 movePos) {
        //Debug.Log(movePos);
        agent.speed = moveSpeed;
        agent.SetDestination(movePos);
        playerAnim.MoveAnimation(agent.speed);
        CurrentEnemyState = EnemyState.Move;
    }

    /// <summary>
    /// NavMesh による経路移動を停止
    /// </summary>
    public void StopNavigation() {
        agent.speed = 0;
        agent.ResetPath();
        playerAnim.MoveAnimation(0);

        InitEnemy();
    }

    /// <summary>
    /// 初期状態の設定
    /// </summary>
    public void InitEnemy() {
        CurrentEnemyState = EnemyState.Idle;
        player = null;
        timer = 0;

        //Debug.Log("Enemy 初期設定完了");
    }

    /// <summary>
    /// 見回り
    /// </summary>
    private void LookAround() {
        timer += Time.deltaTime;

        if (timer >= lookAroundInterval) {
            timer = 0;

            Vector3 direction = new (0, transform.eulerAngles.y + Random.Range(-90.0f, 90.0f), 0);
            transform.DORotate(direction, 1.5f)
                .SetEase(Ease.InQuart).SetLink(gameObject);

            // TODO Interval をランダム化してもよい

        }
    }

　　// 徘徊は別途

}