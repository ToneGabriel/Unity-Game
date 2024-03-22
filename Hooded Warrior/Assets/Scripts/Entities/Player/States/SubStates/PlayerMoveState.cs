﻿
public sealed class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX == 0)
            _player.ChangeState((int)PlayerStateID.Idle);
        else if (_inputY == -1)
            _player.ChangeState((int)PlayerStateID.CrouchMove);
        else if (_isGrounded && _rollInput)
        {
            _player._inputHandler.UseRollInput();
            _player.ChangeState((int)PlayerStateID.Roll);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);
    }
}