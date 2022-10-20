using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyTier;
    [SerializeField] int maxHealth;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int deathMoney;

    int health;
    float positionOnTrack = 0;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        // Calculate position on track (for first targetting)
        positionOnTrack += Time.deltaTime * speed;

        // Testing Death and track position
        if (positionOnTrack > 10f)
        {
            DealDamage(health);
        }
    }

    void PathMove()
    {

    }

    public void DealDamage(int damage)
    {
        // Deal damage
        health -= damage;

        // If enemy is dead
        if (health <= 0)
        {
            // Add to player currency
            PlayerManager.Instance.AddToCurrency(deathMoney);
            // Destroy enemy object
            Destroy(gameObject);
        }
    }

    #region Getters

    public int GetTier() { return enemyTier; }

    public int GetHealth() { return health; }

    public float GetPositionOnTrack() { return positionOnTrack; }

    #endregion
}
