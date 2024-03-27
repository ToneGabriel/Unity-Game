
public sealed class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, string animBoolName)
        : base(player, animBoolName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityY(_player.PlayerData.WallClimbVelocity);

        if (_inputY != 1)
            _player.ChangeState((int)PlayerStateID.WallGrab);
    }
}