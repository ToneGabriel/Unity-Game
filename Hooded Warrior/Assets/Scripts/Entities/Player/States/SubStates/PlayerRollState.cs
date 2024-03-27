
public sealed class PlayerRollState : PlayerAbilityState
{
    public PlayerRollState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(_player.PlayerData.RollVelocity * _player.EntityIntStatusComponents.FacingDirection);
        _player.SetColiderHeight(_player.PlayerData.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.PlayerData.CrouchLightOrbPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_player.PlayerData.RollVelocity * _player.EntityIntStatusComponents.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetVelocityZero();
        _player.SetColiderHeight(_player.PlayerData.StandColliderHeight);
        _player.SetLightOrbPosition(_player.PlayerData.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}