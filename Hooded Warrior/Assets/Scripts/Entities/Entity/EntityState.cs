using UnityEngine;


public abstract class EntityState : State
{
    protected Entity    _entity;
    protected string    _animBoolName;     // Animation bool name for each state
    protected float     _stateStartTime;
    protected bool      _isStateAnimationFinished;

    public EntityState(Entity entity, string animBoolName)
    {
        _entity         = entity;
        _animBoolName   = animBoolName;
    }

    public override void Enter()
    {
        _stateStartTime             = Time.time;
        _isStateAnimationFinished   = false;
        _entity.SetAnimatorBoolParam(_animBoolName, true);

        DoChecks();
    }

    public override void Exit()
    {
        _entity.SetAnimatorBoolParam(_animBoolName, false);
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() => DoChecks();

    public override void DoChecks() { }

    public override void AnimationTrigger() { }

    public override void AnimationFinishTrigger()
    {
        _isStateAnimationFinished = true;
    }
}