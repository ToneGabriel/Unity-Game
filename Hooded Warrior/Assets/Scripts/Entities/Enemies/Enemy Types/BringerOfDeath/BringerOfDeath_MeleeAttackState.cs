using UnityEngine;

public class BringerOfDeath_MeleeAttackState : MeleeAttackState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_MeleeAttack stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bod.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_bod.LookForPlayerState);
        }
    }

    public override void TriggerMeleeAttack()
    {
        base.TriggerMeleeAttack();

        Collider2D detectedObject = Physics2D.OverlapCircle(_bod.MeleeAttackPosition.transform.position, _stateData.AttackRadius, _stateData.WhatIsPlayer);
        if (detectedObject)
            detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }
}
