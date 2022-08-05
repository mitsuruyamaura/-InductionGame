using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    [SerializeField]
    private BoxCollider weaponCollider;

    private PlayerAnimation playerAnim;
    private EnemyNavigationController navigationController;

    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }
        if (!TryGetComponent(out navigationController)) {
            Debug.Log("EnemyNavigationController 取得出来ません。");
        }
    }

    /// <summary>
    /// アニメーションイベント用
    /// </summary>
    /// <param name="switchNo"></param>
    public void SwitchCollider(int switchNo) {
        // 帽子のコライダーのオンオフ切り替え
        weaponCollider.enabled = switchNo == 1 ? true : false;
    }
}
