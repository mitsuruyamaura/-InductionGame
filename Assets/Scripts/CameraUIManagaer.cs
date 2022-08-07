using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraUIManagaer : MonoBehaviour
{
    [SerializeField]
    private Image[] imgCameraFrames;

    [SerializeField]
    private float defaultDuration = 0.5f;

    [SerializeField]
    private float cameraFramePos = 100.0f;

    [SerializeField]
    private float[] defaultCameraFramePoss;


    void Start() {
        defaultCameraFramePoss = new float[imgCameraFrames.Length];
        for (int i = 0; i < imgCameraFrames.Length; i++) {
            defaultCameraFramePoss[i] = imgCameraFrames[i].rectTransform.anchoredPosition.y;// �A���J�[�ݒ肵�Ă���̂ŁA���[�J�����W���_��
        }

        // �f�o�b�O
        //StartCoroutine(DebugCameraFrame());
    }

    /// <summary>
    /// �J�����t���[���̃f�o�b�O
    /// </summary>
    /// <returns></returns>
    private IEnumerator DebugCameraFrame() {
        FrameInCameraFrames();

        yield return new WaitForSeconds(2.0f);

        FrameOutCameraFrames();
    }

    /// <summary>
    /// �J�����t���[���̃t���[���C��
    /// </summary>
    public void FrameInCameraFrames() {
        imgCameraFrames[0].transform.DOLocalMoveY(-cameraFramePos, defaultDuration).SetEase(Ease.Linear).SetRelative();
        imgCameraFrames[1].transform.DOLocalMoveY(cameraFramePos, defaultDuration).SetEase(Ease.Linear).SetRelative();
        Debug.Log("frame in");
    }

    /// <summary>
    /// �J�����t���[���̃t���[���A�E�g
    /// </summary>
    public void FrameOutCameraFrames() {
        for (int i = 0; i < imgCameraFrames.Length; i++) {
            imgCameraFrames[i].transform.DOLocalMoveY(defaultCameraFramePoss[i], defaultDuration).SetEase(Ease.Linear).SetRelative();
        }
        Debug.Log("frame out");
    }
}