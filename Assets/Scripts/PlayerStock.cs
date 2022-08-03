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
            Debug.Log("UIPresenter ���擾�o���܂���B");
            return;
        }
        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }

    /// <summary>
    /// �X�g�b�N�̍X�V
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateStocks(int amount) {
        stocks = Mathf.Clamp(stocks += amount, 0, maxStock);

        // ��ʕ\���X�V
        uiPresenter.PrepareDisplayPlayerStocks(stocks);
    }
}
