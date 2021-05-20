using System;

namespace InfenixTools.DesignPatterns
{
    public abstract class State<T, U> : Singleton<State<T, U>>, IState<U>
    {
        public event Action onEnter;
        public event Action onExit;

        public virtual void Enter(U objectInstance)
        {
            onEnter?.Invoke();
        }

        public abstract void Execute(U objectInstance);

        public virtual void Exit(U objectInstance)
        {
            onExit?.Invoke();
        }
    }
}
