using System;
using System.Collections.Generic;


// EState is the enum used to map states
public class FiniteStateMachine<EState> : State
where EState : Enum
{
    #region Components
    private EState                      _defaultStateID;
    private State                       _currentState   = null;
    private Dictionary<EState, State>   _states         = null;
    #endregion Components 

    #region FSM Late Initialization
    public void InitializeStates(params KeyValuePair<EState, State>[] newStates)
    {
        int stateCount = Enum.GetValues(typeof(EState)).Length;

        if (newStates == null || newStates.Length != stateCount)
            throw new Exception("The number of states must be equal to the enum count!");

        _states = new Dictionary<EState, State>();

        foreach (var pair in newStates)
        {
            EState key  = pair.Key;
            State state = pair.Value;

            if (_states.ContainsKey(key))
                throw new Exception("Duplicate Key!");

            if (state == null)
                throw new Exception("Null State not allowed!");

            _states.Add(key, state);
        }
    }

    public void SetDefaultState(EState stateID)
    {
        _defaultStateID = stateID;
    }
    #endregion FSM Late Initialization

    #region State Interface
    public override void Enter()
    {
        _currentState = _states[_defaultStateID];
        _currentState.Enter();
    }

    public override void Update()
    {
        _currentState.Update();
    }

    public override void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public override void Exit()
    {
        _currentState.Exit();
    }

    public override void AnimationTrigger()
    {
        _currentState.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        _currentState.AnimationFinishTrigger();
    }
    #endregion State Interface

    #region FSM Interface
    public void ChangeState(EState stateID)
    {
        _currentState.Exit();
        _currentState = _states[stateID];
        _currentState.Enter();
    }

    public bool IsStateActive(EState stateID)
    {
        return _currentState == _states[stateID];
    }

    public State CurrentState()
    {
        return _currentState;
    }
    #endregion FSM Interface
}