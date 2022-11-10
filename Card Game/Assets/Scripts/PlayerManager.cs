using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject testTower;
    public Cards testCard;
    public int gridSize = 1;
    public LayerMask placeMask;

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

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 towerPlace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerPlace.x = Mathf.Floor(towerPlace.x / gridSize) * gridSize + gridSize * 0.5f;
            towerPlace.y = Mathf.Floor(towerPlace.y / gridSize) * gridSize + gridSize * 0.5f;

            Collider2D hit = Physics2D.OverlapBox(towerPlace, Vector2.one, 0.0f, placeMask);
            print("Test: " + towerPlace + " Find: " + hit);
            if(!hit || !hit.CompareTag("PathCollision") && !hit.CompareTag("Tower"))
            {
                GameObject t = Instantiate(testTower);
                t.transform.position = new Vector3(towerPlace.x, towerPlace.y, 1.0f);
                t.GetComponent<GenericTower>().data = testCard;
            }

        }
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
