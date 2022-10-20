using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int lives = 10;
    public int Currency { get; set; }

    [Header("UI")]

    [SerializeField]
    TextMeshProUGUI currencyText;
    [SerializeField]
    TextMeshProUGUI livesCount;

    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } set { } }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Currency = 0;
        livesCount.text = "Lives: " + lives;
        currencyText.text = $"${Currency}";
    }

    public void AddToCurrency(int changeAmount)
    {
        Currency += changeAmount;
        currencyText.text = $"${Currency}";
    }

    public void TakeDamage()
    {
        lives--;
        livesCount.text = "Lives: " + lives;
    }
}
