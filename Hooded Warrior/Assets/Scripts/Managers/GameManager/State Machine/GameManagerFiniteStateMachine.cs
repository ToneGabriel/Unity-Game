
public class GameManagerFiniteStateMachine
{
    public GameManagerState CurrentState { get; private set; }

    public void Initialize(GameManagerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(GameManagerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
