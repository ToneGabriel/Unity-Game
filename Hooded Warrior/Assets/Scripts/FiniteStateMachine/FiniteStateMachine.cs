
public sealed class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public void SetInitialState(State newState)
    {
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
