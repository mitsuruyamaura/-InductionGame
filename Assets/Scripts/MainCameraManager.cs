using Cinemachine;
using UnityEngine;


public enum ViewState {
    NormalView,
    TopView,
    FrontView,
}

public class MainCameraManager : MonoBehaviour
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


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// カメラの位置変更
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
    /// カメラ視点切り替えボタンを押下した際の処理
    /// </summary>
    public void ChangeViewNo() {
        viewNo = ++viewNo % cameraViews.Length;
        ChangeView();
    }

    /// <summary>
    /// クリア時のカメラ視点
    /// </summary>
    public void ClearCameraView() {       
        clearCamera.Priority = 15;
        Debug.Log("Clear Camera セット");
    }


    public Transform GetClearCameraTran() {
        return clearCamera.transform;
    }


    public void CutFollowTarget() {
        clearCamera.m_Follow = null;
    }
}