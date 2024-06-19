using UnityEngine;

public class BullMeleeAttackState : EntityModeState
{
    private BullAggroMode       _bullAggroMode;
    private BullAggroModeData   _bullAggroModeData;

    public BullMeleeAttackState(BullAggroMode mode, BullAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _bullAggroMode      = mode;
        _bullAggroModeData  = data;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_isStateAnimationFinished)
        //{
        //    if (_isPlayerInMinAgroRange)
        //        _bull.ChangeState((int)BullStateID.PlayerDetected);
        //    else
        //        _bull.ChangeState((int)BullStateID.LookForPlayer);
        //}
    }

    //public override void TriggerMeleeAttack()
    //{
    //    base.TriggerMeleeAttack();

    //    //Collider2D detectedObject = Physics2D.OverlapCircle(_bull.MeleeAttackPosition.transform.position, _stateData.AttackRadius, _stateData.WhatIsPlayer);
    //    //if(detectedObject)
    //    //    detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    //}
}
