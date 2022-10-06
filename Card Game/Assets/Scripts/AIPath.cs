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

    public Vector2 GetPosition(int node)
    {
        Debug.Assert(node < points.Count);
        return points[node].position;
    }


    public int GetLastNode()
    {
        return points.Count - 1;
    }

    public int GetNextPosition(int targetNode, ref int currentPath, out Vector2 targetPos)
    {
        if (targetNode > currentPath)
        {
            ++currentPath;
            targetPos = points[currentPath].position;
            return currentPath;
        }
        else
        {
            targetPos = points[currentPath].position;
            --currentPath;
            return currentPath + 1;
        }
    }
}
