using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_player.ResetAndDecreaseAmountOfJumpsLeft();
        //_player.SetVelocity(_player.StateData.WallJumpVelocity,
        //                    _player.StateData.WallJumpAngle,
        //                    -_player.FacingDirection);
        //_player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (Time.time >= _stateStartTime + _player.StateData.WallJumpTime)
        //    _isAbilityDone = true;
    }
}