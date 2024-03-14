using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.SetVelocity(_dataPlayer.WallJumpVelocity, _dataPlayer.WallJumpAngle, -_player.FacingDirection);
        _player.JumpState.DecreaseAmountOfJumpsLeft();
        _player.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.Animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player.Rigidbody.velocity.y);
        _player.Animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player.Rigidbody.velocity.x));

        if (Time.time >= StartTime + _dataPlayer.WallJumpTime)
            _isAbilityDone = true;
    }

}
