using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class ModularBehaviour : MonoBehaviour
{
    #region Components & Data
    private int _defaultMainModeID                  = 0;
    private int _currentMainModeID                  = 0;
    private State _currentMode                      = null;
    //private CircularFiniteStateMachine _mainMode    = null;
    //private FiniteStateMachine _eventMode           = null;
    #endregion Components & Data

    #region Unity Functions
    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {
        // set initial state with default
        // all default states should have ID == 0

        //if (!_states.ContainsKey(0))
        //    throw new ArgumentException("No default state with current ID == 0 exists!");



        //_currentStateID = 0;
        //_states[_currentStateID].Enter();
    }

    protected virtual void Start()
    {
        // Empty
    }

    protected virtual void Update()
    {
        // check execution
        if (!FSMUpdateConditions())
            return;

        // state logic
        _currentMode.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // check execution
        if (!FSMFixedUpdateConditions())
            return;

        // state logic
        _currentMode.PhysicsUpdate();
    }
    #endregion Unity Functions

    #region Late Init Functions
    protected void InitializeMainModes(params KeyValuePair<int, State>[] newModes)
    {
        //_mainMode.InitializeOrderedStates(newModes);
    }

    protected void InitializeEventModes(params KeyValuePair<int, State>[] newModes)
    {
        //_eventMode.InitializeStates(newModes);
    }

    protected void SetDefaultMainMode(int modeID) // from main modes
    {
        _defaultMainModeID = modeID;
    }

    protected abstract bool FSMUpdateConditions();      // control update execution

    protected abstract bool FSMFixedUpdateConditions(); // control fixedupdate execution
    #endregion Late Init Functions

    #region External Interface
    public void ChangeToNextMainMode()
    {
        // cyclic transition
        //_currentMode = _mainMode;
        //_mainMode.ChangeNextState();
    }

    public void ChangeToDefaultMainState()
    {
        //_currentMode = _mainMode;
        //_mainMode.ChangeDefaultState();
    }

    public void ChangeToEventMode(int modeID)
    {
        // specific transition
        //_currentMode = _eventMode;
        //_eventMode.ChangeState(modeID);
    }
    #endregion External Interface
}
