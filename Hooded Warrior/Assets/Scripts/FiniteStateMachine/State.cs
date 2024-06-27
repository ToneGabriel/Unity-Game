using System;

// Composition
// States will be inherited by leaf concrete states...
// ...and also by FiniteStateMachine
public abstract class State
{
    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() { }
}
