using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GenericTower owner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GenericEnemy enemy))
        {
            enemy.TakeDamage(owner.data.damage);
            Destroy(gameObject);
        }
    }
}
