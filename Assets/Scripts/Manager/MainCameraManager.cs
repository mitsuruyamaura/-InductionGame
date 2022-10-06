using Cinemachine;
using UnityEngine;


public enum ViewState {
    NormalView,
    TopView,
    FrontView,
}

public class MainCameraManager : MonoBehaviour, IEntryRun
{
    public static MainCameraManager instance;

    [SerializeField]
    private CinemachineVirtualCamera mainCamera;

    public CinemachineVirtualCamera MainCamera { get => mainCamera; }

    [SerializeField]
    private CinemachineVirtualCamera[] cameraViews;

    [SerializeField]
    private int viewNo;

    [SerializeField]
    private CinemachineVirtualCamera clearCamera;

    private CameraUIManagaer cameraUIManagaer;

    /// <summary>
    /// EntryPoint �����s
    /// Start �̑���� Awake �^�C�~���O�ŏ����ݒ�
    /// CameraUIManager �����O�̃^�C�~���O�Ŏ��s���Ă���
    /// </summary>
    public void EntryRun() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        cameraUIManagaer = transform.GetComponentInChildren<CameraUIManagaer>();
        Debug.Log(cameraUIManagaer + " �擾");
    }

    //void Awake() {
    //    if (instance == null) {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else {
    //        Destroy(gameObject);
    //    }

    //    cameraUIManagaer = transform.GetComponentInChildren<CameraUIManagaer>();
    //    Debug.Log(cameraUIManagaer);
    //}

    /// <summary>
    /// �J�����̈ʒu�ύX
    /// </summary>
    public void ChangeView() {
        for (int i = 0; i < cameraViews.Length; i++) {
            if (i == viewNo) {
                cameraViews[i].Priority = 10;
            } else {
                cameraViews[i].Priority = 5;
            }
        }
    }

    /// <summary>
    /// �J�������_�؂�ւ��{�^�������������ۂ̏���
    /// </summary>
    public void ChangeViewNo() {
        viewNo = ++viewNo % cameraViews.Length;
        ChangeView();
    }

    /// <summary>
    /// �N���A���̃J�������_
    /// </summary>
    public void ClearCameraView() {
        // �t���[���C��
        cameraUIManagaer.FrameInCameraFrames();
        clearCamera.Priority = 15;
        Debug.Log("Clear Camera �Z�b�g");
    }


    public Transform GetClearCameraTran() {
        return clearCamera.transform;
    }

    /// <summary>
    /// Follow ���O��(LoolAt ���̃J�����̃u����h��)
    /// </summary>
    public void CutFollowTarget() {
        clearCamera.m_Follow = null;
    }
}