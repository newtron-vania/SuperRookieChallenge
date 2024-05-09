using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : AbstractPathFind
{
    public AStar(float maxDistance, float stepDistance) : base(maxDistance, stepDistance)
    {
    }
    
    public override List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        PriorityQueue<Node> openList = new PriorityQueue<Node>();
        HashSet<Vector2> closedSet = new HashSet<Vector2>();

        Node startNode = new Node(start, 0, Vector3.Distance(start, goal), null);
        openList.Enqueue(startNode);
        Node closestNode = startNode;

        while (openList.Count > 0)
        {
            Node currentNode = openList.Dequeue();

            if (Vector2.Distance(currentNode.Position, goal) < Vector2.Distance(closestNode.Position, goal))
            {
                closestNode = currentNode;
            }

            closedSet.Add(currentNode.Position);

            if (currentNode.Position == goal)
            {
                return RetracePath(startNode, currentNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode, goal))
            {
                if (closedSet.Contains(neighbor.Position)) continue;

                float tentativeGCost = currentNode.GCost + Vector2.Distance(currentNode.Position, neighbor.Position);
                if (tentativeGCost < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.Parent = currentNode;
                    if (!openList.Contains(neighbor))
                    {
                        openList.Enqueue(neighbor);
                    }
                }
            }
        }

        // 목표 지점에 도달할 수 없는 경우, 가장 가까운 위치로 도달하는 경로를 반환
        return RetracePath(startNode, closestNode);
    }
    
    List<Vector3> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;

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
        List<Node> neighbors = new List<Node>();
        Vector3[] directions = {
            new Vector2(-_stepDistance, 0), new Vector2(_stepDistance, 0),
            new Vector2(0, -_stepDistance), new Vector2(0, _stepDistance),
            new Vector2(-_stepDistance, -_stepDistance), new Vector2(-_stepDistance, _stepDistance),
            new Vector2(_stepDistance, -_stepDistance), new Vector2(_stepDistance, _stepDistance)
        };

        foreach (Vector3 direction in directions)
        {
            Vector2 neighborPosition = node.Position + direction;
            if (IsValidPosition(direction, neighborPosition))
            {
                float gCost = node.GCost + Vector2.Distance(node.Position, neighborPosition);
                float hCost = Vector2.Distance(neighborPosition, goal);
                Node neighborNode = new Node(neighborPosition, gCost, hCost, node);
                neighbors.Add(neighborNode);
            }
        }

        return neighbors;
    }
    
    private bool IsValidPosition(Vector2 position, Vector2 target)
    {
        
        Vector2 direction = target - position;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, _stepDistance);
        if (hit)
        {
            // 이동 캐릭터 본인의 콜라이더 무시
            if (_character != null && hit.collider.gameObject == _character.gameObject)
            {
                return true;
            }
            return false;
        }
        return true;
    }
}
