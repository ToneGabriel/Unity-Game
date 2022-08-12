using UnityEngine;

public class Bull_MeleeAttackState : MeleeAttackState
{
    private Bull _bull;

    public Bull_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_MeleeAttack stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bull.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_bull.LookForPlayerState);
        }
    }

    public override void TriggerMeleeAttack()
    {
        base.TriggerMeleeAttack();

        Collider2D detectedObject = Physics2D.OverlapCircle(_bull.MeleeAttackPosition.transform.position, _stateData.AttackRadius, _stateData.WhatIsPlayer);
        if(detectedObject)
            detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }
}
