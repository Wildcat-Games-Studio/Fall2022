using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTower : MonoBehaviour
{
    [SerializeField]
    public Cards data;
    [SerializeField]
    private GameObject _shot;

    float _next_shot_valid = 0.0f;
    private CircleCollider2D _col;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _col = gameObject.AddComponent<CircleCollider2D>();
        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        _col.radius = data.radius;
        _col.isTrigger = true;
        _spriteRenderer.sprite = data.sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > _next_shot_valid)
            Shoot(collision);
    }

    private void Shoot(Collider2D target)
    {
        _next_shot_valid = Time.time + 1.0f / data.attackSpeed;

        Vector2 dir = (target.gameObject.transform.position - transform.position).normalized;

        GameObject spawned = Instantiate(_shot);
        spawned.transform.position = transform.position;
        spawned.GetComponent<Rigidbody2D>().velocity = dir * 20.0f;
        spawned.GetComponent<Shot>().owner = this;
        Destroy(spawned, 10);
    }
}
