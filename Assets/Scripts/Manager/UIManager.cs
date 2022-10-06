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
    /// EntryPoint より実行
    /// Start の代わりに Awake タイミングで初期設定
    /// </summary>
    public void EntryRun() {
        btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeViewNo);

        Debug.Log("UIManager 設定");
    }

    //void Start()
    //{
    //    btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeViewNo);
    //}

    /// <summary>
    /// PlayerStock 数の表示更新
    /// </summary>
    /// <param name="stock"></param>
    public void UpdateDisplayPlayerStocks(int stock) {
        txtPlayerStock.DOCounter(beforeStocks, stock, 0.5f).SetEase(Ease.InQuart);
        beforeStocks = stock;
    }
}