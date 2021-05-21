using System;

namespace InfenixTools.DesignPatterns
{
    public abstract class State<T, U> : Singleton<State<T, U>>, IState<U>
    {
        public static event Action OnEnter;
        public static event Action OnExecute;
        public static event Action OnExit;

        public virtual void Enter(U fre)
        {
            OnEnter?.Invoke();
        }

        public virtual void Execute(U objectInstance)
        {
            OnExecute?.Invoke();
        }

        public virtual void Exit(U objectInstance)
        {
            OnExit?.Invoke();
        }
    }
}
