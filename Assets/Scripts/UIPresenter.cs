using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPresenter : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private PlayerStock playerStock;

    
    void Start()
    {
        // TODO UniRx �g��
    }

    /// <summary>
    /// UniRx �g���܂ŗ��p����
    /// </summary>
    /// <param name="stocks"></param>
    public void PrepareDisplayPlayerStocks(int stocks) {
        uiManager.UpdateDisplayPlayerStocks(stocks);
    }
}