using System;

// Composition
// States will be inherited by leaf concrete states...
// ...and also by FiniteStateMachine
public abstract class State
{
    // Not intended for implementation here
    public virtual void AddNewState(int stateID, State newState) => throw new NotImplementedException();

    public virtual void ChangeState(int stateID) => throw new NotImplementedException();

    // Implement in derived States
    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() { }
}
