
public sealed class PlayerRollState : PlayerAbilityState
{
    public PlayerRollState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(_dataPlayer.RollVelocity * _player.FacingDirection);
        _player.SetColiderHeight(_dataPlayer.CrouchColliderHeight);
        _player.SetLightOrbPosition(_dataPlayer.CrouchLightOrbPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_dataPlayer.RollVelocity * _player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetVelocityZero();
        _player.SetColiderHeight(_dataPlayer.StandColliderHeight);
        _player.SetLightOrbPosition(_dataPlayer.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}