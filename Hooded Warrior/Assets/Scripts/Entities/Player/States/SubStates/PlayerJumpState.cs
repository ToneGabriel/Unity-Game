
public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityY(_dataPlayer.JumpVelocity);
        _isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        _player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
            return true;
        return false;
    }

    public void ResetAmountOfJumpsLeft()
    {
        _amountOfJumpsLeft = _dataPlayer.AmountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        _amountOfJumpsLeft--;
    }

}
