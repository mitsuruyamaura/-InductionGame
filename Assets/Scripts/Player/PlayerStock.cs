using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStock : MonoBehaviour
{
    private UIPresenter uiPresenter;

    [SerializeField]
    private int stocks;

    private int maxStock;

    void Start()
    {
        maxStock = stocks;
        if (!TryGetComponent(out uiPresenter)) {
            Debug.Log("UIPresenter を取得出来ません。");
            return;
        }
        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }

    /// <summary>
    /// ストックの更新
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateStocks(int amount) {
        stocks = Mathf.Clamp(stocks += amount, 0, maxStock);

        // 画面表示更新
        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }
}
