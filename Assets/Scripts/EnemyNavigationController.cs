using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/// <summary>
/// �G�l�~�[�̏��
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
            Debug.Log("NavMeshAgent �擾�o���܂���B");
        }
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        // ������
        InitEnemy();
    }

    void Update()
    {
        if (!agent) {
            return;
        }

        // �A�C�h�����͈�莞�Ԃ��ƂɌ���肷��
        if (CurrentEnemyState == EnemyState.Idle) {
            LookAround();
            return;
        }

        if (!player) {
            return;
        }

        // ��苗���ɋ߂Â�����
        if (player && Vector3.Distance(transform.position, player.transform.position) <= 2.0f) {
            CurrentEnemyState = EnemyState.Attack;
            StartCoroutine(ObserveAttack());
        }

        // �_�E�������U�����̎��͏������Ȃ�
        if (CurrentEnemyState == EnemyState.Down || CurrentEnemyState == EnemyState.Attack) {
            return;
        }

        // ����ȊO(Move)�͈ړ�
        agent.SetDestination(player.transform.position);
    }

    /// <summary>
    /// �U���̊Ď�
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
        Debug.Log("�U���I��");        
    }

    private void OnTriggerEnter(Collider other) {
        // �_�E�������U�����̎��͏������Ȃ�
        if (CurrentEnemyState == EnemyState.Down || CurrentEnemyState == EnemyState.Attack) {
            //Debug.Log(CurrentEnemyState);
            return;
        }

        // ���łɃv���C���[�������Ă���ꍇ�ɂ͏������Ȃ�
        if (player) {
            //Debug.Log(player);
            return;
        }

        // �N�������R���C�_�[�����Q�[���I�u�W�F�N�g���v���C���[�ł��邩����
        if (other.TryGetComponent(out player)) {
            Debug.Log("�v���C���[����");

            // �v���C���[�̏ꍇ�ɂ́A�v���C���[�̈ʒu�� NavMeshAgent �̖ڕW�n�_�Ƃ��ăZ�b�g
            SetNavigation(player.transform.position);
        }
    }

    private void OnTriggerExit(Collider other) {

        // �g���K�[�͈̔͂��炢�Ȃ��Ȃ����R���C�_�[�����Q�[���I�u�W�F�N�g���v���C���[�ł��邩����
        if (other.TryGetComponent(out player)) {
            Debug.Log("�v���C���[������");
            StopNavigation();

            // �����ʒu�̐ݒ肪�Ȃ��ꍇ�ɂ́A���̏�ɂ���
            if (!startTran) {              
                return;
            }
            // �����ʒu�̐ݒ肪����ꍇ�ɂ́A�����ɖ߂�
            agent.SetDestination(startTran.position);
        }
    }

    /// <summary>
    /// NavMesh �ɂ��o�H�ړ��ƈړ��A�j������
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
    /// NavMesh �ɂ��o�H�ړ����~
    /// </summary>
    public void StopNavigation() {
        agent.speed = 0;
        agent.ResetPath();
        playerAnim.MoveAnimation(0);

        InitEnemy();
    }

    /// <summary>
    /// ������Ԃ̐ݒ�
    /// </summary>
    public void InitEnemy() {
        CurrentEnemyState = EnemyState.Idle;
        player = null;
        timer = 0;

        //Debug.Log("Enemy �����ݒ芮��");
    }

    /// <summary>
    /// �����
    /// </summary>
    private void LookAround() {
        timer += Time.deltaTime;

        if (timer >= lookAroundInterval) {
            timer = 0;

            Vector3 direction = new (0, transform.eulerAngles.y + Random.Range(-90.0f, 90.0f), 0);
            transform.DORotate(direction, 1.5f)
                .SetEase(Ease.InQuart).SetLink(gameObject);

            // TODO Interval �������_�������Ă��悢

        }
    }

�@�@// �p�j�͕ʓr

}