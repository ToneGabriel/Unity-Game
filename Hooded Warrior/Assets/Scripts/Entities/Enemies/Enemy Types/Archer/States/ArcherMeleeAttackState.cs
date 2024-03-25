using UnityEngine;

public class ArcherMeleeAttackState : EnemyMeleeAttackState
{
    private Archer _archer;

    public ArcherMeleeAttackState(Archer archer, string animBoolName, Data_MeleeAttack stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_archer.StatusComponents.IsStateAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _archer.ChangeState((int)ArcherStateID.PlayerDetected);
            else
                _archer.ChangeState((int)ArcherStateID.LookForPlayer);
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
