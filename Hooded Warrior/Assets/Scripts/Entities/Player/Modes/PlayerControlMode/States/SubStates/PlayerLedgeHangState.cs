using UnityEngine;

public sealed class PlayerLedgeHangState : PlayerState
{
    public PlayerLedgeHangState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_SetRigidbodyStatic();
        _playerControlMode.Target_SetVelocityZero();
        _playerControlMode.Target_JumpCount = _playerControlModeData.MaxAmountOfJumps - 1;
        // TODO
        // _player.AdvancedStatus.CanDash = true;

        // get corner position
        Vector2 cornerPosition = _playerControlMode.Target_GetDetectedLedgeCornerPosition();

        // set grab position
        _playerControlMode.Target_Position = new Vector2(   cornerPosition.x - (_playerControlMode.Target_FacingDirection * _playerControlModeData.StartOffset.x),
                                                            cornerPosition.y - _playerControlModeData.StartOffset.y);
        
        // set future position after climb
        _playerControlMode.Target_FuturePosition = new Vector2( cornerPosition.x + (_playerControlMode.Target_FacingDirection * _playerControlModeData.StopOffset.x),
                                                                cornerPosition.y + _playerControlModeData.StopOffset.y);
    }

    public override void Update()
    {
        base.Update();

        //if ()   // facing direction is inputx
        //{
        //    // rigidbody remains static
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.LedgeClimb);
        //}
        //else if (_inputY == -1)
        //{
        //    _playerControlMode.Target_SetRigidbodyDynamic();
        //    _playerControlMode.Target_ResetDetectedLedgeCornerPosition();
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.WallSlide);
        //}
        //else if (_jumpInput)
        //{
        //    _playerControlMode.Target_SetRigidbodyDynamic();
        //    _playerControlMode.Target_ResetDetectedLedgeCornerPosition();
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.WallJump);
        //}
        //else
        //{
        //    // wait for input
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
