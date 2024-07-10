using UnityEngine;

public sealed class PlayerLedgeClimbState : PlayerAbilityState
{
    public PlayerLedgeClimbState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();


        Vector2 cornerPosition = _playerControlMode.Target_GetDetectedLedgeCornerPosition();
        _playerControlMode.Target_Position = new Vector2(   cornerPosition.x + (_playerControlMode.Target_FacingDirection * _playerControlModeData.StopOffset.x),
                                                            cornerPosition.y + _playerControlModeData.StopOffset.y);
        _playerControlMode.Target_SetVelocityZero();
        _playerControlMode.Target_SetRigidbodyDynamic();    // was static from ledge hang state
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}