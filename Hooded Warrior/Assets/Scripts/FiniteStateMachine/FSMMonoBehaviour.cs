using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class FSMMonoBehaviour : MonoBehaviour
{
    #region Components & Data
    private FiniteStateMachine      _stateMachine;
    private Dictionary<int, State>  _states;
    #endregion Components & Data

    #region Unity Functions
    protected virtual void Awake()
    {
        _stateMachine   = new FiniteStateMachine();
        _states         = new Dictionary<int, State>();

        InitializeStates();
    }

    protected virtual void OnEnable()
    {
        // set initial state with default
        // all default states should have ID == 0

        if (!_states.ContainsKey(0))
            throw new ArgumentException("No default state with current ID == 0 exists!");

        _stateMachine.SetInitialState(_states[0]);
    }

    protected virtual void Start()
    {
        // Empty
    }

    protected virtual void Update()
    {
        //if (GameManager.Instance.IsGamePaused)
        //    return;

        _stateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        //if (GameManager.Instance.IsGamePaused)
        //    return;

        _stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion Unity Functions

    #region Late Init Functions
    protected abstract void InitializeStates();                 // use AddNewState() in derived class

    protected void AddNewState(int stateID, State newState)     // called in derived class InitializeStates()
    {
        if (_states.ContainsKey(stateID))
            throw new ArgumentException("The state with current ID already exists!");

        _states.Add(stateID, newState);
    }
    #endregion Late Init Functions

    #region External Interface
    public void ChangeState(int stateID)                        // called in individual states
    {
        if (!_states.ContainsKey(stateID))
            throw new ArgumentException("No state with current ID exists!");

        _stateMachine.ChangeState(_states[stateID]);
    }

    public bool IsStateActive(int stateID)
    {
        return _stateMachine.CurrentState == _states[stateID];
    }
    #endregion External Interface
}
