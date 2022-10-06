using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour, IEntryRun
{
    [SerializeField]
    private Button btnChangeCameraRotate;

    [SerializeField]
    private Text txtPlayerStock;

    private int beforeStocks;


    /// <summary>
    /// EntryPoint �����s
    /// Start �̑���� Awake �^�C�~���O�ŏ����ݒ�
    /// </summary>
    public void EntryRun() {
        btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeViewNo);

        Debug.Log("UIManager �ݒ�");
    }

    //void Start()
    //{
    //    btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeViewNo);
    //}

    /// <summary>
    /// PlayerStock ���̕\���X�V
    /// </summary>
    /// <param name="stock"></param>
    public void UpdateDisplayPlayerStocks(int stock) {
        txtPlayerStock.DOCounter(beforeStocks, stock, 0.5f).SetEase(Ease.InQuart);
        beforeStocks = stock;
    }
}