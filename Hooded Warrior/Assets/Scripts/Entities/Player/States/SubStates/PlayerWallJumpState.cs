using UnityEngine;

public sealed class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player._jumpState.ResetAmountOfJumpsLeft();
        _player.SetVelocity(_dataPlayer.WallJumpVelocity, _dataPlayer.WallJumpAngle, -_player.StatusComponents.FacingDirection);
        _player._jumpState.DecreaseAmountOfJumpsLeft();
        _player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player.ObjectComponents.Rigidbody.velocity.y);
        _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player.ObjectComponents.Rigidbody.velocity.x));

        if (Time.time >= _player.StatusComponents.StateStartTime + _dataPlayer.WallJumpTime)
            _isAbilityDone = true;
    }
}