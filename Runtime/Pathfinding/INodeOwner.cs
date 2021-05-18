namespace InfenixTools.Pathfinding
{
    public interface INodeOwner
    {
        void CreateNode();
        void RemoveNode();
        PathNode GetNode();
        void LinkNode();
    }
}
