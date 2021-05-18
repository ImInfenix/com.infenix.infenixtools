namespace InfenixTools.DesignPatterns
{
    public interface IState<T>
    {
        public abstract void Enter(T objectInstance);
        public abstract void Execute(T objectInstance);
        public abstract void Exit(T objectInstance);
    }
}
