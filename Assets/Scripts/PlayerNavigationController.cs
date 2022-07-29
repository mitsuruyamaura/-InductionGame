using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �v���C���[�̏�Ԃ̎��
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
    //private int layerMask = 1 << 6;�@�@�@// �r�b�g���Z�Ń}�X�N���쐬����B���g�p
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
            Debug.Log("PlayerAnimation ���擾�o���܂���B");
        }
        if (!TryGetComponent(out agent)) {
            Debug.Log("NavMeshAgent ���擾�o���܂���B");
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
            // �ړ��ł���n�_�����肷��
            CheckRoot();
        }

        // �ړI�n�ɋ߂Â�����
        if (Vector3.Distance(transform.position, destination) <= 0.15f) {
            // ��~������
            //agent.speed = 0;
            playerAnim.MoveAnimation(0);
            agent.ResetPath();
        }
    }

    /// <summary>
    /// OffMeshLink �̊Ď�
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveOffMeshLink() {

        //// OffMeshLink �̋������I�[�g�̏ꍇ�ɂ͂��̏����͍s��Ȃ�
        //if (agent.autoTraverseOffMeshLink) {
        //    yield break;
        //}

        while (true) {
            while (agent.isOnOffMeshLink) {
                if (!playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                    // Trigger ���Ə�肭�J�ڂł��Ȃ����߁ABool �ɕύX
                    //playerAnim.ChangeAnimationFromTrigger(PlayerAnimationState.Jump);

                    playerAnim.ChangeAnimationBool(PlayerAnimationState.Jump, true);
                }              
                yield return null;
            }
            if (playerAnim.GetAnimator() != null && playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                playerAnim.ChangeAnimationBool(PlayerAnimationState.Jump, false);
                //Debug.Log("OffMesh�ɂ��ړ��I��");
            }


            // NavMesh �ɂ��ړ����~(Path ���͕ێ����Ă���)
            //agent.isStopped = true;

            //// TODO ����������ꍇ�ɂ͒���������

            // �e���|�[�g����
            //yield return OffMeshLinkProcess(agent.currentOffMeshLinkData.endPos);

            // OffMeshLink �̌v�Z�I��
            //agent.CompleteOffMeshLink();

            // NavMesh �ɂ��ړ��ĊJ
            //agent.isStopped = false;

            yield return null;
        }

        //IEnumerator OffMeshLinkProcess(Vector3 targetPos) {
        //    yield return null;
        //}
    }

    /// <summary>
    /// �N���b�N�����n�_�� NavMesh ��̌o�H������
    /// </summary>
    private void CheckRoot() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray.direction);
        //Debug.Log(LayerMask.NameToLayer("Floor"));
        
        // �ړ��\�Ȓn�_�ł����
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100, LayerMask.GetMask(layerName))) {

            // ��Q��������ꍇ�ɂ͈ړ������Ȃ�
            if (hit.collider.TryGetComponent(out ObstacleBase obstacleBase)) {
                return;
            }

            // NavMesh �ňړ�
            NavigationMove(hit.point);
            // �ړI�n��o�^
            destination = hit.point;
        }
    }

    /// <summary>
    /// NavMesh �ɂ��o�H�ړ��ƈړ��A�j������
    /// </summary>
    /// <param name="movePos"></param>
    private void NavigationMove(Vector3 movePos) {
        //Debug.Log(movePos);
        agent.speed = moveSpeed;
        agent.SetDestination(movePos);
        playerAnim.MoveAnimation(agent.speed);
    }
}
