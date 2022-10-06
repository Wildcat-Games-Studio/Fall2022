using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    AIPath path;
    Vector2 target;

    int currentPath = 0;
    int targetNode = 0;
    int nextNode = 0;

    public float distFromTarget;
    public float speed;

    void Start()
    {
        path = GameObject.Find("Path").GetComponent<AIPath>();

        targetNode = path.GetLastNode();
        transform.position = path.GetPosition(0);
        path.GetNextPosition(targetNode, ref currentPath, out target);
    }

    void Update()
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(dir * Time.deltaTime * speed);

        if(Vector2.Distance(transform.position, target) < distFromTarget)
        {
            if (nextNode == path.GetLastNode())
            {
                Destroy(gameObject);
                PlayerManager.Instance.TakeDamage();
                return;
            }
            nextNode = path.GetNextPosition(targetNode, ref currentPath, out target);
        }
    }
}
