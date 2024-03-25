
public sealed class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _player.ChangeState((int)PlayerStateID.Move);
        else if (_player.EntityIntStatusComponents.IsStateAnimationFinished)
            _player.ChangeState((int)PlayerStateID.Idle);
    }
}