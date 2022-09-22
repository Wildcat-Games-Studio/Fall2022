using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] int currency;

    [Header("UI")]
    [SerializeField] TMP_Text currencyText;

    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;

        currencyText.text = $"${currency}";
    }

    public void AddToCurrency(int changeAmount)
    {
        currency += changeAmount;

        currencyText.text = $"${currency}";
    }

    #region Getters

    public int GetCurrency() { return currency; }

    #endregion
}
