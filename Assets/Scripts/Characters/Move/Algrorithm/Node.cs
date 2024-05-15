using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    public float GCost;
    public float HCost;
    public Node Parent;
    public Vector3 Position;

    public Node(Vector3 position, float gCost, float hCost, Node parent)
    {
        Position = position;
        GCost = gCost;
        HCost = hCost;
        Parent = parent;
    }

    public float FCost => GCost + HCost;

    public int CompareTo(Node other)
    {
        return FCost.CompareTo(other.FCost);
    }
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private readonly List<T> elements = new();

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
        var ci = elements.Count - 1;
        while (ci > 0)
        {
            var pi = (ci - 1) / 2;
            if (elements[ci].CompareTo(elements[pi]) >= 0) break;
            var tmp = elements[ci];
            elements[ci] = elements[pi];
            elements[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        var li = elements.Count - 1;
        var frontItem = elements[0];
        elements[0] = elements[li];
        elements.RemoveAt(li);

        --li;
        var pi = 0;
        while (true)
        {
            var ci = pi * 2 + 1;
            if (ci > li) break;
            var rc = ci + 1;
            if (rc <= li && elements[rc].CompareTo(elements[ci]) < 0)
                ci = rc;
            if (elements[pi].CompareTo(elements[ci]) <= 0) break;
            var tmp = elements[pi];
            elements[pi] = elements[ci];
            elements[ci] = tmp;
            pi = ci;
        }

        return frontItem;
    }

    public T Peek()
    {
        var frontItem = elements[0];
        return frontItem;
    }

    public bool Contains(T item)
    {
        return elements.Contains(item);
    }
}