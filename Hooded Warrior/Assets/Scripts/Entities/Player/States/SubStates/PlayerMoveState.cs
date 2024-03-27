
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
            InputManager.Instance.UseRollInput();
            _player.ChangeState((int)PlayerStateID.Roll);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_player.PlayerData.MovementVelocity * _inputX);
    }
}