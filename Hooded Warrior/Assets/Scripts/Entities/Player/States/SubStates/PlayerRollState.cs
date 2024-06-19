
public sealed class PlayerRollState : PlayerAbilityState
{
    //public PlayerRollState(Player player, string animBoolName)
    //    : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetColiderHeight(_player.StateData.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //_player.SetVelocityX(_player.StateData.RollVelocity * _player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        //_player.SetVelocityZero();
        _player.SetColiderHeight(_player.StateData.StandColliderHeight);
        _player.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}