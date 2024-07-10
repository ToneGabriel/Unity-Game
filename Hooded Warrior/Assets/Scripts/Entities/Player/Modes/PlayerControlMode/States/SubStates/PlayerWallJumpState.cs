using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_ResetAndDecreaseAmountOfJumpsLeft();
        //_playerControlMode.Target_SetVelocity(  _player.StateData.WallJumpVelocity,
        //                                        _player.StateData.WallJumpAngle,
        //                                        -_player.FacingDirection);
        _playerControlMode.Target_Flip();
    }

    public override void Update()
    {
        base.Update();

        //if (Time.time >= _stateStartTime + _player.StateData.WallJumpTime)
        //    _isAbilityDone = true;
    }
}