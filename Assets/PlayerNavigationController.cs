using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // �ړ��ł���n�_�����肷��
            CheckRoot();
        }

        // �ړI�n�ɋ߂Â�����
        if (Vector3.Distance(transform.position, destination) <= 0.15f) {
            // ��~������
            //agent.speed = 0;
            playerAnim.MoveAnimation(0);
        }
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
