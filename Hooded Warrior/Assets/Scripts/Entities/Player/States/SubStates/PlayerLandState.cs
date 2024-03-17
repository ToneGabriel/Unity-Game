
public sealed partial class Player
{
    private sealed partial class PlayerLandState
    {
        public PlayerLandState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_inputX != 0)
                _stateMachine.ChangeState(_player._moveState);
            else if (_isAnimationFinished)
                _stateMachine.ChangeState(_player._idleState);
        }
    }
}
