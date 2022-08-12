
using UnityEngine;

public abstract class EnemyState : State
{
    protected Enemy _enemy;                                                                  // Reference to enemy base class

    public EnemyState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName)
        : base(stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.Animator.SetBool(_animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.Animator.SetBool(_animBoolName, false);
    }
}
