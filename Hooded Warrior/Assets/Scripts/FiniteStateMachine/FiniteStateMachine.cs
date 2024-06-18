using System;
using UnityEngine;


public abstract class FiniteStateMachine : State
{
    private int     _defaultStateID = -1;
    private int     _currentStateID = -1;
    private State[] _states         = null;

    public abstract string[] AnimatorParameterNames { get; }

    protected virtual void Start()
    {
        // Check integrity

        foreach (var state in _states)
            if (state == null)
                throw new Exception();

        if (_defaultStateID == -1)
            throw new Exception();
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

    protected void CreateStateArray(int size)
    {
        if (_states != null)
            throw new Exception();

        if (size <= 0)
            throw new Exception();

        _states = new State[size];
    }

    public override void AddNewState(int stateID, State state)
    {
        if (_states == null)
            throw new Exception();

        if (state == null)
            throw new Exception();

        if (0 > stateID || _states.Length <= stateID || _states[stateID] != null)
            throw new Exception();

        _states[stateID] = state;
    }

    protected void SetDefaultState(int stateID)
    {
        if (0 > stateID || _states.Length <= stateID || _states[stateID] == null)
            throw new Exception();

        _defaultStateID = stateID;
    }

    public override void ChangeState(int stateID)
    {
        CheckStateID(stateID);

        _states[_currentStateID].Exit();
        _currentStateID = stateID;
        _states[_currentStateID].Enter();
    }

    public bool IsStateActive(int stateID)
    {
        CheckStateID(stateID);

        return _states[_currentStateID] == _states[stateID];
    }

    public State CurrentState()
    {
        return _states[_currentStateID];
    }

    private void CheckStateID(int stateID)
    {
        if (0 > stateID || _states.Length <= stateID)
            throw new ArgumentException("No state with ID exists!");
    }
}