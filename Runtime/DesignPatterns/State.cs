using System;

namespace InfenixTools.DesignPatterns
{
    public abstract class State<SingletonType, StateType> : Singleton<State<SingletonType, StateType>>, IState<StateType>
    {
        public static event Action OnEnter;
        public static event Action OnExecute;
        public static event Action OnExit;

        public virtual void Enter(StateType objectInstance)
        {
            OnEnter?.Invoke();
        }

        public virtual void Execute(StateType objectInstance)
        {
            OnExecute?.Invoke();
        }

        public virtual void Exit(StateType objectInstance)
        {
            OnExit?.Invoke();
        }
    }
}
