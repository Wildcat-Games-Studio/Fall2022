using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPath : MonoBehaviour
{
    private List<Transform> points = new List<Transform>();
    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if(child.CompareTag("PathNode"))
            {
                points.Add(child);
            }
        }
    }

    public Vector2 GetEntryNode()
    {
        return points[0].position;
    }

    public int GetTarget(int currentTarget, out Vector2 targetPos)
    {
        if(currentTarget >= 0)
        {
            int res = currentTarget + 1;
            if (res < points.Count)
            {
                targetPos = points[res].position;
                return res;
            }
        }
        targetPos = Vector2.zero;
        return -1;
    }
}
