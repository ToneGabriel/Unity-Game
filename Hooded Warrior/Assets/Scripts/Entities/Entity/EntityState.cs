using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract partial class Entity
{
    protected abstract partial class EntityState
    {
        public float StartTime { get; protected set; }

        protected FiniteStateMachine _stateMachine;                                              // Reference to state change engine
        protected string _animBoolName;                                                          // Animation bool name for each state
        protected bool _isAnimationFinished;
        protected Vector2 _workspaceVector2;

        public EntityState(FiniteStateMachine stateMachine, string animBoolName)
        {
            _stateMachine = stateMachine;
            _animBoolName = animBoolName;
        }

        public override void Enter()                                                             // Called when enter state
        {
            StartTime = Time.time;
            DoChecks();
        }

        public override void Exit() { }                                                          // Called when exit state

        public override void LogicUpdate() { }                                                   // Update (only while current state is active)

        public override void PhysicsUpdate() => DoChecks();                                      // FixedUpdate (only while current state is active)

        public override void DoChecks() { }                                                      // Checks for position/attack

        public override void AnimationTrigger() { }

        public override void AnimationFinishTrigger()
        {
            _isAnimationFinished = true;
        }
    }
}