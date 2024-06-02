using System;
using UnityEngine;


public abstract class FSMMonoBehaviour : MonoBehaviour
{
    #region Components & Data
    FiniteStateMachine _fsm;
    #endregion Components & Data

    #region Unity Functions
    protected virtual void Awake()
    {
        //_fsm = new FiniteStateMachine();

        FSMInitializeModes();
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

        //// state logic
        //_fsm.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // check execution
        if (!FSMFixedUpdateConditions())
            return;

        //// state logic
        //_fsm.PhysicsUpdate();
    }
    #endregion Unity Functions

    #region Late Init Functions
    protected abstract void FSMInitializeModes();       // use AddNewMode() in derived class

    protected abstract bool FSMUpdateConditions();      // control update execution

    protected abstract bool FSMFixedUpdateConditions(); // control fixedupdate execution

    public void AddNewMode(int modeID, FiniteStateMachine newMode = null)   // called in derived class InitializeModes()
    {
        //_fsm.AddNewState(modeID, newMode ?? new FiniteStateMachine());
        // newMode != null ? newMode : new FiniteStateMachine()
    }

    public void AddNewState(int destModeID, int stateID, State newState)    // called in derived class InitializeStates()
    {
        // get mode and add new state to it
        //_fsm.GetState(destModeID).AddNewState(stateID, newState);
    }

    //protected void AddNewTransition(int fromStateID, int toStateID, Func<bool> condition)
    //{
    //    if (fromStateID == toStateID)
    //        throw new ArgumentException("Cannot make transition to self!");

    //    if (!_states.ContainsKey(fromStateID) || !_states.ContainsKey(toStateID))
    //        throw new ArgumentException("No states with 'from' or 'to' ID exists!");

    //    if (!_transitions.ContainsKey(fromStateID)) // no transitions exists from this state
    //        _transitions.Add(fromStateID, new Dictionary<int, Func<bool>>());

    //    if (!_transitions[fromStateID].ContainsKey(toStateID)) // no duplicate transition from -> to
    //        _transitions[fromStateID].Add(toStateID, condition);
    //    else
    //        throw new ArgumentException("Duplicate transition!");
    //}
    #endregion Late Init Functions

    #region External Interface
    public void ChangeMode(int modeID)
    {
        _fsm.ChangeState(modeID);
    }

    public void ChangeState(int stateID)
    {
        // get current mode and change state
        _fsm.CurrentState().ChangeState(stateID);
    }

    public bool IsStateActive(int stateID)
    {
        return _fsm.IsStateActive(stateID);
    }
    #endregion External Interface
}
