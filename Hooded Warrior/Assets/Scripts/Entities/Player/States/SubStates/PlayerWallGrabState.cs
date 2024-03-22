using UnityEngine;

public sealed class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector3 _workspaceVector3;

    public PlayerWallGrabState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _workspaceVector3 = _player.transform.position;
        HoldPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        if (_inputY > 0)
            _player.ChangeState((int)PlayerStateID.WallClimb);
        else if (_inputY < 0 || !_grabInput)
            _player.ChangeState((int)PlayerStateID.WallSlide);
    }

    private void HoldPosition()
    {
        _player.transform.position = _workspaceVector3;
        _player.SetVelocityZero();
    }

}