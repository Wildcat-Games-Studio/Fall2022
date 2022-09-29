using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    AIPath path;
    Vector2 target;
    int currentNode = 0;

    public float distFromTarget;
    public float speed;

    void Start()
    {
        path = GameObject.Find("Path").GetComponent<AIPath>();
        target = path.GetEntryNode();
    }

    void Update()
    {
        if(currentNode != -1)
        {
            Vector2 dir = (target - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(dir * Time.deltaTime * speed);

            if(Vector2.Distance(transform.position, target) < distFromTarget)
            {
                currentNode = path.GetTarget(currentNode, out target);
                print(currentNode);
            }
        }

        if (currentNode == -1)
        {
            GameObject.Destroy(gameObject);
            PlayerManager.Instance.TakeDamage();
        }
    }



}
