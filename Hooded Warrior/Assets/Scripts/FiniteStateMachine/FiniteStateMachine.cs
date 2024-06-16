using System;
using UnityEngine;


public abstract class FiniteStateMachine : MonoBehaviour
{
    private int     _defaultStateID = -1;
    private int     _currentStateID = -1;
    private State[] _states         = null;

    public abstract string[] AnimatorParameterNames { get; }

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        // Check integrity

        foreach (var state in _states)
            if (state == null)
                throw new Exception();

        if (_defaultStateID == -1)
            throw new Exception();
    }

    protected virtual void OnEnable()
    {
        _currentStateID = _defaultStateID;
        _states[_currentStateID].Enter();
    }

    protected virtual void OnDisable()
    {
        _states[_currentStateID].Exit();
        _currentStateID = _defaultStateID;
    }

    protected virtual void Update()
    {
        _states[_currentStateID].LogicUpdate();
    }

    protected virtual void FixedUpdate()
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

    protected void AddNewState(int stateID, State state)
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

    public void ChangeState(int stateID)
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