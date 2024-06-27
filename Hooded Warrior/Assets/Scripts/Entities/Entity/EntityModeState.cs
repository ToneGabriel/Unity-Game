using System;
using UnityEngine;


public abstract class EntityModeState<EState> : State
where EState : Enum
{
    private readonly EntityMode<EState> _entityMode;
    private readonly string             _animBoolName;     // Animator parameter bool name for each state
    
    protected float     _stateStartTime;
    protected bool      _isStateAnimationFinished;

    public EntityModeState(EntityMode<EState> mode, string animBoolName)
    {
        _entityMode     = mode;
        _animBoolName   = animBoolName;
    }

    public override void Enter()
    {
        _stateStartTime             = Time.time;
        _isStateAnimationFinished   = false;
        _entityMode.Target_SetAnimatorBoolParam(_animBoolName, true);

        DoChecks();
    }

    public override void Exit()
    {
        _entityMode.Target_SetAnimatorBoolParam(_animBoolName, false);
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() => DoChecks();

    public override void AnimationTrigger() { }

    public override void AnimationFinishTrigger()
    {
        _isStateAnimationFinished = true;
    }

    protected virtual void DoChecks() { }
}