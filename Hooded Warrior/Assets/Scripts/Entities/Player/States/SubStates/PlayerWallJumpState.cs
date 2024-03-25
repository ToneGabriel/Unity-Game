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

        _player.EntityExtObjComponents.Animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player.EntityExtObjComponents.Rigidbody.velocity.y);
        _player.EntityExtObjComponents.Animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player.EntityExtObjComponents.Rigidbody.velocity.x));

        if (Time.time >= _player.EntityIntStatusComponents.StateStartTime + _dataPlayer.WallJumpTime)
            _isAbilityDone = true;
    }
}