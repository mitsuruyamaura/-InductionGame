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
        // TODO UniRx 使う
    }

    /// <summary>
    /// UniRx 使うまで利用する
    /// </summary>
    /// <param name="stocks"></param>
    public void PrepareDisplayPlayerStocks(int stocks) {
        uiManager.UpdateDisplayPlayerStocks(stocks);
    }
}

