using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private PlayerAnimation playerAnim;
    private EnemyNavigationController navigationController;
    private float downInterval = 2.0f;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation 取得出来ません。");
        }

        if (!TryGetComponent(out navigationController)) {
            Debug.Log("EnemyNavigationController 取得出来ません。");
        }
    }


    private void OnTriggerEnter(Collider other) {
        // 帽子に Rigidbody がないので、ColliderEnter では接触しない
        //Debug.Log("Collider Hit");

        if (other.TryGetComponent(out Hat hat)) {

            // TODO
            Debug.Log("気絶");

            StartCoroutine(DownEnemy());
        }
    }

    /// <summary>
    /// ダウン→起き上がる
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownEnemy() {
        // ステート切り替え　重複処理防止
        navigationController.CurrentEnemyState = EnemyState.Down;

        // 移動停止
        navigationController.StopNavigation();

        // ダウンアニメ再生
        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, true);

        // 起き上がるまでの待機時間
        yield return new WaitForSeconds(downInterval);

        // TODO 倒している場合には破壊


        // それ以外はその場に起き上がる
        playerAnim.ChangeAnimationBool(PlayerAnimationState.Down, false);

        // TODO あるいは、初期の位置に戻す


        // 初期状態に戻す
        navigationController.InitEnemy();
    }
}