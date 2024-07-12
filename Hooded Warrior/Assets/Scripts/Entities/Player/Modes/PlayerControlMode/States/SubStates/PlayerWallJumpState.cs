using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_JumpCount = _playerControlModeData.MaxAmountOfJumps - 1;

        _playerControlMode.Target_Flip();
        _playerControlMode.Target_SetVelocity(  _playerControlModeData.WallJumpVelocity,
                                                _playerControlModeData.WallJumpAngle,
                                                _playerControlMode.Target_FacingDirection);
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= _stateStartTime + _playerControlModeData.WallJumpTime)
            _isAbilityDone = true;
    }
}