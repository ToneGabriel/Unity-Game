using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private int _inputX;
    private int _inputY;

    public PlayerLedgeClimbState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.JumpState.DecreaseAmountOfJumpsLeft();
        _player.DashState.ResetCanDash();

        _player.SetVelocityZero();
        _player.transform.position = _detectedPosition;
        _cornerPosition = _player.DetermineCornerPosition();
        _startPosition.Set(_cornerPosition.x - (_player.FacingDirection * _dataPlayer.StartOffset.x), _cornerPosition.y - _dataPlayer.StartOffset.y);
        _stopPosition.Set(_cornerPosition.x + (_player.FacingDirection * _dataPlayer.StopOffset.x), _cornerPosition.y + _dataPlayer.StopOffset.y);

        _player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if(_isClimbing)
        {
            _player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
            _stateMachine.ChangeState(_player.IdleState);
        else
        {
            _inputX = _player.InputHandler.NormalizedInputX;
            _inputY = _player.InputHandler.NormalizedInputY;
            _jumpInput = _player.InputHandler.JumpInput;

            _player.SetVelocityZero();
            _player.transform.position = _startPosition;

            if (_inputX == _player.FacingDirection && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                _player.Animator.SetBool(PlayerControllerParameters.ClimbLedge_b, true);
            }
            else if (_inputY == -1 && _isHanging && !_isClimbing)
                _stateMachine.ChangeState(_player.WallSlideState);
            else if (_jumpInput && !_isClimbing)
                _stateMachine.ChangeState(_player.WallJumpState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _player.Animator.SetBool(PlayerControllerParameters.ClimbLedge_b, false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public void SetDetectedPosition(Vector2 pos)
    {
        _detectedPosition = pos;
    }

}
