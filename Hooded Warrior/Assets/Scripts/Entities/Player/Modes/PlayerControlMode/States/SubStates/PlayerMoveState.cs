
public sealed class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Update()
    {
        base.Update();

        //if (_inputX == 0)
        //    _player.ChangeState((int)PlayerStateID.Idle);
        //else if (_inputY == -1)
        //    _player.ChangeState((int)PlayerStateID.CrouchMove);
        //else if (_isGrounded && _rollInput)
        //{
        //    InputManager.Instance.UseRollInput();
        //    _player.ChangeState((int)PlayerStateID.Roll);
        //}
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_player.SetVelocityX(_player.StateData.MovementVelocity * _inputX);
    }
}