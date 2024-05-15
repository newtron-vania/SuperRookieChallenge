using System.Collections.Generic;
using UnityEngine;

public class AStar : AbstractPathFind
{
    public AStar(float maxDistance, float stepDistance) : base(maxDistance, stepDistance)
    {
    }

    public override List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        var openList = new PriorityQueue<Node>();
        var closedSet = new HashSet<Vector2>();

        var startNode = new Node(start, 0, Vector3.Distance(start, goal), null);
        openList.Enqueue(startNode);
        var closestNode = startNode;

        while (openList.Count > 0)
        {
            var currentNode = openList.Dequeue();

            if (Vector2.Distance(currentNode.Position, goal) < Vector2.Distance(closestNode.Position, goal))
                closestNode = currentNode;

            closedSet.Add(currentNode.Position);

            if (currentNode.Position == goal) return RetracePath(startNode, currentNode);

            foreach (var neighbor in GetNeighbors(currentNode, goal))
            {
                if (closedSet.Contains(neighbor.Position)) continue;

                var tentativeGCost = currentNode.GCost + Vector2.Distance(currentNode.Position, neighbor.Position);
                if (tentativeGCost < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.Parent = currentNode;
                    if (!openList.Contains(neighbor)) openList.Enqueue(neighbor);
                }
            }
        }

        // 목표 지점에 도달할 수 없는 경우, 가장 가까운 위치로 도달하는 경로를 반환
        return RetracePath(startNode, closestNode);
    }

    private List<Vector3> RetracePath(Node startNode, Node endNode)
    {
        var path = new List<Vector3>();
        var currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private List<Node> GetNeighbors(Node node, Vector3 goal)
    {
        var neighbors = new List<Node>();
        Vector3[] directions =
        {
            new Vector2(-_stepDistance, 0), new Vector2(_stepDistance, 0),
            new Vector2(0, -_stepDistance), new Vector2(0, _stepDistance),
            new Vector2(-_stepDistance, -_stepDistance), new Vector2(-_stepDistance, _stepDistance),
            new Vector2(_stepDistance, -_stepDistance), new Vector2(_stepDistance, _stepDistance)
        };

        foreach (var direction in directions)
        {
            Vector2 neighborPosition = node.Position + direction;
            if (IsValidPosition(direction, neighborPosition))
            {
                var gCost = node.GCost + Vector2.Distance(node.Position, neighborPosition);
                var hCost = Vector2.Distance(neighborPosition, goal);
                var neighborNode = new Node(neighborPosition, gCost, hCost, node);
                neighbors.Add(neighborNode);
            }
        }

        return neighbors;
    }

    private bool IsValidPosition(Vector2 position, Vector2 target)
    {
        var direction = target - position;

        var hit = Physics2D.Raycast(position, direction, _stepDistance);
        if (hit)
        {
            // 이동 캐릭터 본인의 콜라이더 무시
            if (_character != null && hit.collider.gameObject == _character.gameObject) return true;
            return false;
        }

        return true;
    }
}