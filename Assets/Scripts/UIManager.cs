using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button btnChangeCameraRotate;

    [SerializeField]
    private Text txtPlayerStock;

    private int beforeStocks;


    void Start()
    {
        btnChangeCameraRotate.onClick.AddListener(MainCameraManager.instance.ChangeViewNo);
    }

    /// <summary>
    /// PlayerStock êîÇÃï\é¶çXêV
    /// </summary>
    /// <param name="stock"></param>
    public void UpdateDisplayPlayerStocks(int stock) {
        txtPlayerStock.DOCounter(beforeStocks, stock, 0.5f).SetEase(Ease.InQuart);
        beforeStocks = stock;
    }
}