
public sealed partial class Player
{
    private sealed partial class PlayerWallClimbState
    {
        public PlayerWallClimbState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _player.SetVelocityY(_dataPlayer.WallClimbVelocity);

            if (_inputY != 1)
                _stateMachine.ChangeState(_player._wallGrabState);
        }
    }
}