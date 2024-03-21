
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
        ValidateState(stateID);

        CurrentState = _states[stateID];
        CurrentState.Enter();
    }

    public void ChangeState(int stateID)
    {
        ValidateState(stateID);

        CurrentState.Exit();
        CurrentState = _states[stateID];
        CurrentState.Enter();
    }

    public void AddNewState(int stateID, State newState)
    {
        ValidateID(stateID);

        _states[stateID] = newState;
    }

    private void ValidateID(int stateID)
    {
        if (stateID < 0 || stateID >= _states.Length)
            throw new System.Exception("Invalid ID");
    }

    private void ValidateState(int stateID)
    {
        ValidateID(stateID);

        if (_states[stateID] == null)
            throw new System.Exception("Unregistered State!");
    }
}
