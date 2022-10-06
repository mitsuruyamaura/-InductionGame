using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ClearManager : MonoBehaviour
{
    private PlayerAnimation playerAnim;


    void Start()
    {
        if (!TryGetComponent(out playerAnim)) {
            Debug.Log("PlayerAnimation �擾�o���܂���B");
        }
        // �f�o�b�O�p
        //StartCoroutine(PrepareClearOperationAsync());
    }

    /// <summary>
    /// �N���A�̏���
    /// </summary>
    public IEnumerator PrepareClearOperationAsync() {
        // �N���A���_�̃J�������Z�b�g
        MainCameraManager.instance.ClearCameraView();

        yield return new WaitForSeconds(1.5f);

        // �J�����̃^�[�Q�b�g�Ǐ]���~
        MainCameraManager.instance.CutFollowTarget();

        // �J�����̕�������
        transform.DOLookAt(MainCameraManager.instance.GetClearCameraTran().position, 1.0f)
            .SetEase(Ease.InQuart)
            .OnComplete(() => 
            {
                // �N���A�A�j���Đ�
                playerAnim.ChangeAnimationBool(PlayerAnimationState.Clear, true);
            });
    }
}
