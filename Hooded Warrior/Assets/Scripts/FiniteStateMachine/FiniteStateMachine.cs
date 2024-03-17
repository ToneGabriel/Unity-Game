
public sealed class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State otherState)
    {
        CurrentState.Exit();
        CurrentState = otherState;
        CurrentState.Enter();
    }
}
