using UnityEngine;

public sealed class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _workspaceVector2;

    public PlayerWallGrabState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_workspaceVector2 = _playerControlMode.Target_Position;
        HoldPosition();
    }

    public override void Update()
    {
        base.Update();

        HoldPosition();

        if (_inputY > 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.WallClimb);
        else if (_inputY < 0 || !_grabInput)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.WallSlide);
    }

    private void HoldPosition()
    {
        //_player.transform.position = _workspaceVector3;
        _playerControlMode.Target_SetVelocityZero();
    }

}