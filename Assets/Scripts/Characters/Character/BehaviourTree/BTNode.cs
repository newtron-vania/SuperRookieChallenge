using System;
using System.Collections.Generic;

public interface IBTNode
{
		// 노드 실행 결과 상태
    public enum ENodeState
    {
        ENS_Running,
        ENS_Success,
        ENS_Failure
    }
		// 노드 실행
    public ENodeState Evaluate();
}

// 노드 실행 성공 여부 반환
public class ConditionNode : IBTNode
{
    private readonly Func<bool> _condition;

    public ConditionNode(Func<bool> condition)
    {
        _condition = condition;
    }

    public IBTNode.ENodeState Evaluate()
    {
        return _condition.Invoke() ? IBTNode.ENodeState.ENS_Success : IBTNode.ENodeState.ENS_Failure;
    }
}

// 이벤트의 무조건적인 실행 노드
public class ActionNode : IBTNode
{
    private readonly Action _action;

    public ActionNode(Action action)
    {
        _action = action;
    }

    public IBTNode.ENodeState Evaluate()
    {
        _action();
        return IBTNode.ENodeState.ENS_Success;
    }
}

// 자식 노드에서 진행중이거나 실행 성공한 노드가 존재할 시 바로 반환
public sealed class SelectorNode : IBTNode
{
    private readonly List<IBTNode> _childs;

    public SelectorNode(List<IBTNode> childs)
    {
        _childs = childs;
    }

    public IBTNode.ENodeState Evaluate()
    {
        if (_childs == null)
            return IBTNode.ENodeState.ENS_Failure;

        foreach (var child in _childs)
            switch (child.Evaluate())
            {
                case IBTNode.ENodeState.ENS_Running:
                    return IBTNode.ENodeState.ENS_Running;
                case IBTNode.ENodeState.ENS_Success:
                    return IBTNode.ENodeState.ENS_Success;
            }

        return IBTNode.ENodeState.ENS_Failure;
    }
}

// 자식 노드에서 진행 중이거나 실패한 행동이 존재할 경우 반환, 성공한 노드가 있다면 진행
public sealed class SequenceNode : IBTNode
{
    private readonly List<IBTNode> _childs;

    public SequenceNode(List<IBTNode> childs)
    {
        _childs = childs;
    }

    public IBTNode.ENodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
            return IBTNode.ENodeState.ENS_Failure;

        foreach (var child in _childs)
            switch (child.Evaluate())
            {
                case IBTNode.ENodeState.ENS_Running:
                    return IBTNode.ENodeState.ENS_Running;
                case IBTNode.ENodeState.ENS_Success:
                    continue;
                case IBTNode.ENodeState.ENS_Failure:
                    return IBTNode.ENodeState.ENS_Failure;
            }

        return IBTNode.ENodeState.ENS_Success;
    }
}