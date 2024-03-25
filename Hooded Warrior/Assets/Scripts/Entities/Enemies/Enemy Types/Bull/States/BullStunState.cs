
public class BullStunState : EnemyStunState
{
    private Bull _bull;

    public BullStunState(Bull bull, string animBoolName, Data_Stun stateData)
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isStunTimeOver)
        {
            if (_isPlayerInMeleeRange)
                _bull.ChangeState((int)BullStateID.MeleeAttack);
            else if (_isPlayerInMinAgroRange)
                _bull.ChangeState((int)BullStateID.Charge);
            else
            {
                //_bull.LookForPlayerState.SetTurnImmediately(true);
                _bull.ChangeState((int)BullStateID.LookForPlayer);
            }
        }
    }
}
