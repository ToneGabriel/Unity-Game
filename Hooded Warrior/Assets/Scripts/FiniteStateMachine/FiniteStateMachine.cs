using System.Collections.Generic;
using System;

// Composition
// Controlled by FSMMonoBehaviour
public class FiniteStateMachine : State
{
    protected int                       _defaultStateID;
    protected int                       _currentStateID;
    protected Dictionary<int, State>    _states;

    public FiniteStateMachine()
    {
        _defaultStateID = -1;
        _currentStateID = -1;
        _states         = new Dictionary<int, State>();
    }

    public override void AddNewState(int stateID, State newState)
    {
        if (_states.ContainsKey(stateID))
            throw new ArgumentException("The state with current ID already exists!");

        _states.Add(stateID, newState);
    }

    public override void ChangeState(int stateID)
    {
        if (!_states.ContainsKey(stateID))
            throw new ArgumentException("No state with current ID exists!");

        _states[_currentStateID].Exit();
        _currentStateID = stateID;
        _states[_currentStateID].Enter();
    }

    public void SetDefaultState(int stateID = 0)
    {
        if (!_states.ContainsKey(stateID))
            throw new ArgumentException("No state with current ID exists!");

        _defaultStateID = stateID;
    }

    public bool IsStateActive(int stateID)
    {
        return _states[_currentStateID] == _states[stateID];
    }

    public State GetState(int stateID)
    {
        if (!_states.ContainsKey(stateID))
            throw new ArgumentException("No state with current ID exists!");

        return _states[stateID];
    }

    public State CurrentState()
    {
        return _states[_currentStateID];
    }

    public override void Enter()
    {
        _currentStateID = _defaultStateID;
        _states[_currentStateID].Enter();
    }

    public override void Exit()
    {
        _states[_currentStateID].Exit();
        _currentStateID = _defaultStateID;
    }

    public override void LogicUpdate()
    {
        _states[_currentStateID].LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        _states[_currentStateID].PhysicsUpdate();
    }
}