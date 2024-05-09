using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector3 Position;
    public float GCost;
    public float HCost;
    public float FCost => GCost + HCost;
    public Node Parent;

    public Node(Vector3 position, float gCost, float hCost, Node parent)
    {
        Position = position;
        GCost = gCost;
        HCost = hCost;
        Parent = parent;
    }

    public int CompareTo(Node other)
    {
        return FCost.CompareTo(other.FCost);
    }
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> elements = new List<T>();

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
        int ci = elements.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (elements[ci].CompareTo(elements[pi]) >= 0) break;
            T tmp = elements[ci]; elements[ci] = elements[pi]; elements[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        int li = elements.Count - 1;
        T frontItem = elements[0];
        elements[0] = elements[li];
        elements.RemoveAt(li);

        --li;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && elements[rc].CompareTo(elements[ci]) < 0)
                ci = rc;
            if (elements[pi].CompareTo(elements[ci]) <= 0) break;
            T tmp = elements[pi]; elements[pi] = elements[ci]; elements[ci] = tmp;
            pi = ci;
        }

        return frontItem;
    }

    public T Peek()
    {
        T frontItem = elements[0];
        return frontItem;
    }

    public bool Contains(T item)
    {
        return elements.Contains(item);
    }
}