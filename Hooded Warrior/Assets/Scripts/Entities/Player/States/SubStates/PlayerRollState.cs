
public sealed class PlayerRollState : PlayerAbilityState
{
    public PlayerRollState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetColiderHeight(_player.Data.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.Data.CrouchLightOrbPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_player.Data.RollVelocity * _player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetVelocityZero();
        _player.SetColiderHeight(_player.Data.StandColliderHeight);
        _player.SetLightOrbPosition(_player.Data.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}