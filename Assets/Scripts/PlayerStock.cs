using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStock : MonoBehaviour
{
    private UIPresenter uiPresenter;

    [SerializeField]
    private int stocks;

    void Start()
    {
        if (!TryGetComponent(out uiPresenter)) {
            Debug.Log("UIPresenter ���擾�o���܂���B");
            return;
        }
        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }


    public void UpdateStocks(int amount) {
        stocks += amount;

        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }
}
