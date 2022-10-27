using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    [SerializeField]
    int enemyTier;
    [SerializeField]
    float health;
    [SerializeField]
    float speed;
    [SerializeField]
    int deathMoney;
    [SerializeField]
    float distFromTarget = 0.05f;
    [SerializeField]
    int deathField = 1;

    AIPath _path;
    Vector2 _target;

    int _current_path = 0;
    int _target_node = 0;
    int _next_node = 0;


    void Start()
    {
        _path = GameObject.Find("Path").GetComponent<AIPath>();

        _target_node = _path.GetLastNode();
        transform.position = _path.GetPosition(0);
        _path.GetNextPosition(_target_node, ref _current_path, out _target);
    }

    void Update()
    {
        Vector2 dir = (_target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(dir * Time.deltaTime * speed);

        if(Vector2.Distance(transform.position, _target) < distFromTarget)
        {
            if (_next_node == _path.GetLastNode())
            {
                Destroy(gameObject);
                PlayerManager.Instance.TakeDamage();
                WaveManager.Instance.EnemyKilled(this);
                return;
            }
            _next_node = _path.GetNextPosition(_target_node, ref _current_path, out _target);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        TakeDamage(collision.gameObject.GetComponent<Shot>().owner.data.damage);
    }

    void TakeDamage(float val)
    {
        health -= val;
        if(health <= 0)
        {
            Destroy(gameObject);
            PlayerManager.Instance.AddToCurrency(deathField);
        }
    }
}
