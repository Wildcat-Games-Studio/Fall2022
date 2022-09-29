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

    public static int lives = 10;
    public int livesTemp;

    public static TextMeshPro livesCount;
    

    void Awake()
    {
        lives = livesTemp;
        livesCount.text = "Lives: " + lives;

        Instance = this;

        currencyText.text = $"${currency}";

        livesCount = GetComponentInChildren<TextMeshPro>();
    }

    public void AddToCurrency(int changeAmount)
    {
        currency += changeAmount;

        currencyText.text = $"${currency}";
    }

    public static void TakeDamage()
    {
        lives--;
        livesCount.text = "Lives: " + lives;
    }


    #region Getters

    public int GetCurrency() { return currency; }

    #endregion
}
