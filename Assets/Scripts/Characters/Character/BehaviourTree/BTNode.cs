using System;
using System.Collections.Generic;

public interface IBTNode
{
    public enum ENodeState
    {
        ENS_Running,
        ENS_Success,
        ENS_Failure
    }

    public ENodeState Evaluate();
}

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