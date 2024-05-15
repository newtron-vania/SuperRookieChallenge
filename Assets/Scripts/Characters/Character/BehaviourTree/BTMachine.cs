public class BTMachine
{
    private readonly IBTNode _rootNode;

    public BTMachine(IBTNode rootNode)
    {
        _rootNode = rootNode;
    }

    public void Operate()
    {
        _rootNode.Evaluate();
    }
}