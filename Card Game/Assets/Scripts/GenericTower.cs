using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTower : MonoBehaviour
{
    [SerializeField]
    Cards data;

    CircleCollider2D col;

    void Start()
    {
        col = gameObject.AddComponent<CircleCollider2D>();
        col.radius = data.radius;
        col.isTrigger = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemy in range trigger!");
    }
}
