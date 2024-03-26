using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player._jumpState.ResetAmountOfJumpsLeft();
        _player.SetVelocity(_dataPlayer.WallJumpVelocity, _dataPlayer.WallJumpAngle, -_player.EntityIntStatusComponents.FacingDirection);
        _player._jumpState.DecreaseAmountOfJumpsLeft();
        _player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _player.EntityIntStatusComponents.StateStartTime + _dataPlayer.WallJumpTime)
            _isAbilityDone = true;
    }
}