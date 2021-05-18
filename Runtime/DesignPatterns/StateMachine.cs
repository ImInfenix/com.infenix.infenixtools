namespace InfenixTools.DesignPatterns
{
    public class StateMachine<T>
    {
        private IState<T> currentState;

        public StateMachine(IState<T> initialState, T objectInstance)
        {
            currentState = initialState;
            currentState.Enter(objectInstance);
        }

        public void Update(T objectInstance)
        {
            currentState.Execute(objectInstance);
        }

        public void ChangeState(IState<T> newState, T objectInstance)
        {
            currentState.Exit(objectInstance);
            currentState = newState;
            currentState.Enter(objectInstance);
        }
    }
}
