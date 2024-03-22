
public sealed class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    private State[] _states;

    public FiniteStateMachine(int stateCount)
    {
        _states = new State[stateCount];
    }

    public void InitializeState(int stateID)
    {
        CheckRegisteredState(stateID);

        CurrentState = _states[stateID];
        CurrentState.Enter();
    }

    public void ChangeState(int stateID)
    {
        CheckRegisteredState(stateID);

        CurrentState.Exit();
        CurrentState = _states[stateID];
        CurrentState.Enter();
    }

    public void AddNewState(int stateID, State newState)
    {
        CheckUnregisteredState(stateID);

        _states[stateID] = newState;
    }

    private void CheckID(int stateID)
    {
        if (stateID < 0 || stateID >= _states.Length)
            throw new System.Exception("Invalid ID");
    }

    private void CheckRegisteredState(int stateID)
    {
        CheckID(stateID);

        if (null == _states[stateID])
            throw new System.Exception("Unregistered State!");
    }

    private void CheckUnregisteredState(int stateID)
    {
        CheckID(stateID);

        if (null != _states[stateID])
            throw new System.Exception("State Overwrite!");
    }
}
