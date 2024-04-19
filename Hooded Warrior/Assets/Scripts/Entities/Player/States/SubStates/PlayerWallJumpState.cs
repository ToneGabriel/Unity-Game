using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.ResetAndDecreaseAmountOfJumpsLeft();
        _player.SetVelocity(_player.Data.WallJumpVelocity,
                            _player.Data.WallJumpAngle,
                            -_player.FacingDirection);
        _player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _player.Data.WallJumpTime)
            _isAbilityDone = true;
    }
}