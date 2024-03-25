using UnityEngine;

public class BringerOfDeathMeleeAttackState : EnemyMeleeAttackState
{
    private BringerOfDeath _bod;

    public BringerOfDeathMeleeAttackState(BringerOfDeath bod, string animBoolName, Data_MeleeAttack stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_bod.EntityIntStatusComponents.IsStateAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
            else
                _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
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
