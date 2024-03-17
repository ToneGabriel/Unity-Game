
public sealed partial class Player
{
    private sealed partial class PlayerWallGrabState
    {
        public PlayerWallGrabState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        { }

        public override void Enter()
        {
            base.Enter();

            _workspaceVector2 = _player.transform.position;
            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            HoldPosition();

            if (_inputY > 0)
                _stateMachine.ChangeState(_player._wallClimbState);
            else if (_inputY < 0 || !_grabInput)
                _stateMachine.ChangeState(_player._wallSlideState);
        }

        private void HoldPosition()
        {
            _player.transform.position = _workspaceVector2;
            _player.SetVelocityZero();
        }

    }
}