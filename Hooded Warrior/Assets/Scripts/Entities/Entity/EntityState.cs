using UnityEngine;


public abstract class EntityState : State
{
    protected Entity _entity;
    protected string _animBoolName;     // Animation bool name for each state

    public EntityState(Entity entity, string animBoolName)
    {
        _entity         = entity;
        _animBoolName   = animBoolName;
    }

    public override void Enter()
    {
        _entity.EntityIntStatusComponents.StateStartTime             = Time.time;
        _entity.EntityIntStatusComponents.IsStateAnimationFinished   = false;
        _entity.EntityIntObjComponents.Animator.SetBool(_animBoolName, true);

        DoChecks();
    }

    public override void Exit()
    {
        _entity.EntityIntObjComponents.Animator.SetBool(_animBoolName, false);
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() => DoChecks();

    public override void DoChecks() { }

    public override void AnimationTrigger() { }

    public override void AnimationFinishTrigger()
    {
        _entity.EntityIntStatusComponents.IsStateAnimationFinished = true;
    }
}