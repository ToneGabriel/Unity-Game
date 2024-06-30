using UnityEngine;

public class BringerOfDeathMeleeAttackState : EntityModeState<BringerOfDeathAggroMode.StateID>
{
    private readonly BringerOfDeathAggroMode        _bringerOfDeathAggroMode;
    private readonly BringerOfDeathAggroModeData    _bringerOfDeathAggroModeData;

    public BringerOfDeathMeleeAttackState(BringerOfDeathAggroMode mode, BringerOfDeathAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _bringerOfDeathAggroMode        = mode;
        _bringerOfDeathAggroModeData    = data;
    }

    public override void Update()
    {
        base.Update();

        //if (_isStateAnimationFinished)
        //{
        //    if (_isPlayerInMinAgroRange)
        //        _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
        //    else
        //        _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
        //}
    }

    //public override void TriggerMeleeAttack()
    //{
    //    base.TriggerMeleeAttack();

    //    //Collider2D detectedObject = Physics2D.OverlapCircle(_bod.MeleeAttackPosition.transform.position, _stateData.AttackRadius, _stateData.WhatIsPlayer);
    //    //if (detectedObject)
    //    //    detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    //}
}
