using UnityEngine;

public sealed partial class Player
{
    private sealed partial class PlayerWallJumpState
    {
        public PlayerWallJumpState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        { }

        public override void Enter()
        {
            base.Enter();

            _player._jumpState.ResetAmountOfJumpsLeft();
            _player.SetVelocity(_dataPlayer.WallJumpVelocity, _dataPlayer.WallJumpAngle, -_player.FacingDirection);
            _player._jumpState.DecreaseAmountOfJumpsLeft();
            _player.Flip();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player._animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player._rigidbody.velocity.y);
            _player._animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player._rigidbody.velocity.x));

            if (Time.time >= StartTime + _dataPlayer.WallJumpTime)
                _isAbilityDone = true;
        }
    }
}
