
public sealed class PlayerRollState : PlayerAbilityState
{
    public PlayerRollState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_playerControlMode.Target_SetColliderHight(_player.StateData.CrouchColliderHeight);
        //_playerControlMode.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_playerControlMode.Target_SetVelocityX(_player.StateData.RollVelocity * _player.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        _playerControlMode.Target_SetVelocityZero();
        //_playerControlMode.SetColiderHeight(_player.StateData.StandColliderHeight);
        //_playerControlMode.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}