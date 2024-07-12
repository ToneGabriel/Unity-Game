
public sealed class PlayerRollState : PlayerAbilityState
{
    public PlayerRollState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseRollInput();

        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.CrouchColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.CrouchLightOrbPosition);
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
        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.StandColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.StandLightOrbPosition);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}