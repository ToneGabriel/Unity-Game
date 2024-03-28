using UnityEngine;

public class BullMeleeAttackState : EnemyMeleeAttackState
{
    private Bull _bull;

    public BullMeleeAttackState(Bull bull, string animBoolName, Data_MeleeAttack stateData) 
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isStateAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _bull.ChangeState((int)BullStateID.PlayerDetected);
            else
                _bull.ChangeState((int)BullStateID.LookForPlayer);
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
