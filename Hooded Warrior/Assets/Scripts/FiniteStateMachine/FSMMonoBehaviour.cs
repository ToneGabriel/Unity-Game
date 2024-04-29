using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class FSMMonoBehaviour : MonoBehaviour
{
    #region Components & Data
    private int                                             _currentStateID;
    private Dictionary<int, State>                          _states;
    private Dictionary<int, Dictionary<int, Func<bool>>>    _transitions;
    #endregion Components & Data

    #region Unity Functions
    protected virtual void Awake()
    {
        _states         = new Dictionary<int, State>();
        _transitions    = new Dictionary<int, Dictionary<int, Func<bool>>>();
        _currentStateID = -1;

        FSMInitializeStates();
        FSMInitializeTransitions();
    }

    protected virtual void OnEnable()
    {
        // set initial state with default
        // all default states should have ID == 0

        if (!_states.ContainsKey(0))
            throw new ArgumentException("No default state with current ID == 0 exists!");

        _currentStateID = 0;
        _states[_currentStateID].Enter();
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
        _states[_currentStateID].LogicUpdate();

        // check transitions
        foreach (var trs in _transitions[_currentStateID])
            if (trs.Value())  // condition for transition is true
            {
                ChangeState(trs.Key);
                return;
            }
    }

    protected virtual void FixedUpdate()
    {
        // check execution
        if (!FSMFixedUpdateConditions())
            return;

        // state logic
        _states[_currentStateID].PhysicsUpdate();
    }
    #endregion Unity Functions

    #region Late Init Functions
    protected abstract void FSMInitializeStates();              // use AddNewState() in derived class

    protected abstract void FSMInitializeTransitions();         // use AddNewTransition() in derived class

    protected abstract bool FSMUpdateConditions();              // control update execution

    protected abstract bool FSMFixedUpdateConditions();         // control fixedupdate execution

    protected void AddNewState(int stateID, State newState)     // called in derived class InitializeStates()
    {
        if (_states.ContainsKey(stateID))
            throw new ArgumentException("The state with current ID already exists!");

        _states.Add(stateID, newState);
    }

    protected void AddNewTransition(int fromStateID, int toStateID, Func<bool> condition)
    {
        if (fromStateID == toStateID)
            throw new ArgumentException("Cannot make transition to self!");

        if (!_states.ContainsKey(fromStateID) || !_states.ContainsKey(toStateID))
            throw new ArgumentException("No states with 'from' or 'to' ID exists!");

        if (!_transitions.ContainsKey(fromStateID)) // no transitions exists from this state
            _transitions.Add(fromStateID, new Dictionary<int, Func<bool>>());

        if (!_transitions[fromStateID].ContainsKey(toStateID)) // no duplicate transition from -> to
            _transitions[fromStateID].Add(toStateID, condition);
        else
            throw new ArgumentException("Duplicate transition!");
    }
    #endregion Late Init Functions

    #region External Interface
    public void ChangeState(int stateID)
    {
        if (!_states.ContainsKey(stateID))
            throw new ArgumentException("No state with current ID exists!");

        _states[_currentStateID].Exit();
        _currentStateID = stateID;
        _states[_currentStateID].Enter();
    }

    public bool IsStateActive(int stateID)
    {
        return _states[_currentStateID] == _states[stateID];
    }
    #endregion External Interface
}
