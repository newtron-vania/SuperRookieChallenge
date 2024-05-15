using System.Collections.Generic;
using UnityEngine;

public class JPS : AbstractPathFind
{
    private HashSet<Vector3> closedSet;
    private PriorityQueue<Node> openList;

    private Vector3 start;

    public JPS(float maxDistance, float stepDistance) : base(maxDistance, stepDistance)
    {
    }

    public override List<Vector3> FindPath(Vector3 start, Vector3 target)
    {
        this.start = start;
        var path = new List<Vector3>();
        // 시작점과 목표점 사이의 거리가 _maxDistance보다 크면 빈 경로를 반환
        if (Vector3.Distance(start, target) > _maxDistance)
        {
            Debug.Log("Too Far to target");
            return path;
        }

        openList = new PriorityQueue<Node>();
        closedSet = new HashSet<Vector3>();

        var startNode = new Node(start, 0, Vector3.Distance(start, target), null);
        openList.Enqueue(startNode);

        while (openList.Count > 0)
        {
            if (openList.Count > 1000) return path;
            var currentNode = openList.Dequeue();

            closedSet.Add(currentNode.Position);

            if ((currentNode.Position - target).sqrMagnitude <= _stepDistance * _stepDistance)
                return RetracePath(startNode, currentNode);

            foreach (var neighbor in GetJumpPoints(currentNode, target))
            {
                if (closedSet.Contains(neighbor.Position)) continue;

                if (!openList.Contains(neighbor)) openList.Enqueue(neighbor);
            }
        }

        return path;
    }

    // 경로를 역추적하여 최종 경로 리스트를 반환
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

    // 현재 노드로부터 점프 포인트를 구함
    private List<Node> GetJumpPoints(Node currentNode, Vector3 goal)
    {
        var jumpPoints = new List<Node>();

        foreach (var direction in GetNeighbors(currentNode))
        {
            var jumpPoint = GetJumpPoint(currentNode, direction, goal);
            if (jumpPoint != null) jumpPoints.Add(jumpPoint);
        }

        return jumpPoints;
    }

    // 다음 점프 포인트를 찾거나 null을 반환
    private Node GetJumpPoint(Node currentNode, Vector3 direction, Vector3 goal)
    {
        var nextPosition = currentNode.Position + direction * _stepDistance;
        var nextNode = new Node(nextPosition, currentNode.GCost + _stepDistance, Vector3.Distance(nextPosition, goal),
            currentNode);

        if (!IsValidPosition(currentNode.Position, nextPosition)) return null;

        if ((nextPosition - goal).sqrMagnitude <= _stepDistance * _stepDistance) return nextNode;

        if (HasForcedNeighbor(currentNode, direction)) return nextNode;

        if (direction.x != 0 && direction.y != 0)
        {
            var horizontalJumpPoint = GetJumpPoint(nextNode, new Vector3(direction.x, 0), goal);
            var verticalJumpPoint = GetJumpPoint(nextNode, new Vector3(0, direction.y), goal);

            if (horizontalJumpPoint != null || verticalJumpPoint != null) return nextNode;
        }

        return GetJumpPoint(nextNode, direction, goal);
    }


    // 강제 이웃을 확인하는 함수
    private bool HasForcedNeighbor(Node currentNode, Vector3 direction)
    {
        var diagonalDirection = new Vector3(direction.x, direction.y);
        var orthogonalDirection1 = new Vector3(direction.x, 0);
        var orthogonalDirection2 = new Vector3(0, direction.y);

        var diagonalNeighbor = currentNode.Position + diagonalDirection;
        var orthogonalNeighbor1 = currentNode.Position + orthogonalDirection1;
        var orthogonalNeighbor2 = currentNode.Position + orthogonalDirection2;

        return (IsValidPosition(currentNode.Position, diagonalNeighbor) &&
                !IsValidPosition(currentNode.Position, orthogonalNeighbor1)) ||
               (IsValidPosition(currentNode.Position, diagonalNeighbor) &&
                !IsValidPosition(currentNode.Position, orthogonalNeighbor2));
    }

    // 노드 주변의 가능한 모든 방향을 반환
    private List<Vector3> GetNeighbors(Node node)
    {
        var directions = new List<Vector3>
        {
            new(-_stepDistance, 0), new(_stepDistance, 0),
            new(0, -_stepDistance), new(0, _stepDistance),
            new(-_stepDistance, -_stepDistance), new(-_stepDistance, _stepDistance),
            new(_stepDistance, -_stepDistance), new(_stepDistance, _stepDistance)
        };

        return directions;
    }

    // 지정된 위치가 유효한지 확인
    private bool IsValidPosition(Vector3 position, Vector3 target)
    {
        if ((start - target).magnitude > _maxDistance) return false;
        Vector2 direction = target - position;

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