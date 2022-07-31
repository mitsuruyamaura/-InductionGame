using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button btnChangeCameraRotate;


    void Start()
    {
        btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeView);
    }
}