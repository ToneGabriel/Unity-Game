using System;
using System.Collections.Generic;


public class FiniteStateMachine<EState> : State
where EState : Enum
{
    #region Components
    private EState                      _defaultStateID;
    private State                       _currentState   = null;
    private Dictionary<EState, State>   _states         = null;

    public int StateCount { get { return _states.Count; } }
    #endregion Components 

    #region FSM Late Initialization
    public void InitializeStates(params KeyValuePair<EState, State>[] newStates)
    {
        if (newStates == null)
            throw new Exception();

        _states = new Dictionary<EState, State>();

        foreach (var pair in newStates)
        {
            EState key  = pair.Key;
            State state = pair.Value;

            if (state == null)
                throw new Exception("Null State not allowed!");

            if (_states[key] != null)
                throw new Exception("Duplicate Key!");

            _states.Add(key, state);
        }
    }

    public void SetDefaultState(EState stateID)
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
    public void ChangeState(EState stateID)
    {
        CheckStateID(stateID);

        _currentState.Exit();
        _currentState = _states[stateID];
        _currentState.Enter();
    }

    public bool IsStateActive(EState stateID)
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
    private void CheckStateID(EState stateID)
    {
        if (_states.ContainsKey(stateID))
            throw new ArgumentException("No state with ID exists!");
    }
    #endregion Helpers
}