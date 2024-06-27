using System;
using System.Collections.Generic;


public class FiniteStateMachine : State
{
    #region Components
    private int     _defaultStateID = 0;
    private State   _currentState   = null;
    private State[] _states         = null;

    public int StateCount { get { return _states.Length; } }
    #endregion Components 

    #region FSM Late Initialization
    public void InitializeStates(params KeyValuePair<int, State>[] newStates)
    {
        if (newStates == null)
            throw new Exception();

        _states = new State[newStates.Length];

        foreach (var pair in newStates)
        {
            int key     = pair.Key;
            State state = pair.Value;

            if (state == null)
                throw new Exception("Null State not allowed!");

            if (0 > key || _states.Length <= key || _states[key] != null)
                throw new Exception("Key outside range or duplicate!");

            _states[key] = state;
        }
    }

    public void SetDefaultState(int stateID)
    {
        CheckStateID(stateID);

        _defaultStateID = stateID;
    }
    #endregion FSM Late Initialization

    #region State Interface
    public override void Enter()
    {
        _currentState = _states[_defaultStateID];
        _currentState.Enter();
    }

    public override void Exit()
    {
        _currentState.Exit();
    }

    public override void LogicUpdate()
    {
        _currentState.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        _currentState.PhysicsUpdate();
    }
    #endregion State Interface

    #region FSM Interface
    public void ChangeState(int stateID)
    {
        CheckStateID(stateID);

        _currentState.Exit();
        _currentState = _states[stateID];
        _currentState.Enter();
    }

    public bool IsStateActive(int stateID)
    {
        CheckStateID(stateID);

        return _currentState == _states[stateID];
    }

    public State CurrentState()
    {
        return _currentState;
    }
    #endregion FSM Interface

    #region Helpers
    private void CheckStateID(int stateID)
    {
        if (0 > stateID || _states.Length <= stateID)
            throw new ArgumentException("No state with ID exists!");
    }
    #endregion Helpers
}