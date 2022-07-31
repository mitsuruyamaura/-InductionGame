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


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ÉJÉÅÉâÇÃà íuïœçX
    /// </summary>
    public void ChangeView() {
        viewNo = ++viewNo % cameraViews.Length;

        for (int i = 0; i < cameraViews.Length; i++) {
            if (i == viewNo) {
                cameraViews[i].Priority = 10;
            } else {
                cameraViews[i].Priority = 5;
            }
        }
    }
}