using UnityEngine;

public abstract class State
{
    public float StartTime { get; protected set; }

    protected FiniteStateMachine _stateMachine;                                              // Reference to state change engine
    protected string _animBoolName;                                                          // Animation bool name for each state
    protected bool _isAnimationFinished;
    protected Vector2 _workspaceVector2;

    public State(FiniteStateMachine stateMachine, string animBoolName)
    {
        _stateMachine = stateMachine;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()                                                             // Called when enter state
    {
        StartTime = Time.time;
        DoChecks();
    }

    public virtual void Exit() { }                                                          // Called when exit state

    public virtual void LogicUpdate() { }                                                   // Update (only while current state is active)

    public virtual void PhysicsUpdate() => DoChecks();                                      // FixedUpdate (only while current state is active)

    public virtual void DoChecks() { }                                                      // Checks for position/attack

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger()
    {
        _isAnimationFinished = true;
    }

}
