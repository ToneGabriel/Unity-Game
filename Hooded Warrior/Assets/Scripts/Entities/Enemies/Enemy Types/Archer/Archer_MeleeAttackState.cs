using UnityEngine;

public class Archer_MeleeAttackState : MeleeAttackState
{
    private Archer _archer;

    public Archer_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_MeleeAttack stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_archer.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_archer.LookForPlayerState);
        }
    }

    public override void TriggerMeleeAttack()
    {
        base.TriggerMeleeAttack();

        Collider2D detectedObject = Physics2D.OverlapCircle(_archer.MeleeAttackPosition.transform.position, _stateData.AttackRadius, _stateData.WhatIsPlayer);
        if (detectedObject)
            detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }
}
