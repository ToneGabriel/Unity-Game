using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // TODO
        //_player._jumpState.ResetAmountOfJumpsLeft();
        //_player._jumpState.DecreaseAmountOfJumpsLeft();
        _player.SetVelocity(_player.PlayerData.WallJumpVelocity, _player.PlayerData.WallJumpAngle, -_player.EntityIntStatusComponents.FacingDirection);
        _player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _player.PlayerData.WallJumpTime)
            _isAbilityDone = true;
    }
}