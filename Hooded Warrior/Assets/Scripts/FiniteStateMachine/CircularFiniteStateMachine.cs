using System.Collections.Generic;


public class CircularFiniteStateMachine : State
{
    private int _currentStateID     = 0;
    private FiniteStateMachine _fsm = null;

    public int StateCount { get { return _fsm.StateCount; } }

    public CircularFiniteStateMachine() { }

    public void InitializeOrderedStates(params KeyValuePair<int, State>[] newStates)
    {
        _fsm.InitializeStates(newStates);
        _fsm.SetDefaultState(0);
    }

    #region State Interface
    public override void Enter()
    {
        _fsm.Enter();
    }

    public override void Exit()
    {
        _fsm.Exit();
    }

    public override void LogicUpdate()
    {
        _fsm.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        _fsm.PhysicsUpdate();
    }
    #endregion State Interface

    public void ChangeNextState()
    {
        _currentStateID = (_currentStateID + 1) % StateCount;
        _fsm.ChangeState(_currentStateID);
    }

    public void ChangeDefaultState()
    {
        _currentStateID = 0;
        _fsm.ChangeState(_currentStateID);
    }
}
