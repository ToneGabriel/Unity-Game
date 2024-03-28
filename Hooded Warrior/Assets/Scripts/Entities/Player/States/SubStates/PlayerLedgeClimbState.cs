using UnityEngine;

public sealed class PlayerLedgeClimbState : PlayerState
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

    public PlayerLedgeClimbState(Player player, string animBoolName)
        : base(player, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        // TODO
        //_player._jumpState.ResetAmountOfJumpsLeft();
        //_player._jumpState.DecreaseAmountOfJumpsLeft();
        //_player._dashState.ResetCanDash();

        _player.SetVelocityZero();
        _player.transform.position = _detectedPosition;
        _cornerPosition = _player.DetermineCornerPosition();
        _startPosition.Set( _cornerPosition.x - (_player.EntityIntStatusComponents.FacingDirection * _player.PlayerData.StartOffset.x),
                            _cornerPosition.y - _player.PlayerData.StartOffset.y);
        _stopPosition.Set(  _cornerPosition.x + (_player.EntityIntStatusComponents.FacingDirection * _player.PlayerData.StopOffset.x),
                            _cornerPosition.y + _player.PlayerData.StopOffset.y);

        _player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            _player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isStateAnimationFinished)
            _player.ChangeState((int)PlayerStateID.Idle);
        else
        {
            _inputX     = InputManager.Instance.NormalizedInputX;
            _inputY     = InputManager.Instance.NormalizedInputY;
            _jumpInput  = InputManager.Instance.JumpInput;

            _player.SetVelocityZero();
            _player.transform.position = _startPosition;

            if (_player.IsFacingInput(_inputX) && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                _player.SetAnimatorBoolParam(PlayerControllerParameters.ClimbLedge_b, true);
            }
            else if (_inputY == -1 && _isHanging && !_isClimbing)
                _player.ChangeState((int)PlayerStateID.WallSlide);
            else if (_jumpInput && !_isClimbing)
                _player.ChangeState((int)PlayerStateID.WallJump);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _player.SetAnimatorBoolParam(PlayerControllerParameters.ClimbLedge_b, false);
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