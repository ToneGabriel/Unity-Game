
public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _stateMachine.ChangeState(_player.MoveState);
        else if (_isAnimationFinished)
            _stateMachine.ChangeState(_player.IdleState);
    }
}
