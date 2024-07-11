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

        _playerControlMode.Target_Position = _playerControlMode.Target_FuturePosition;  // future position set in LedgeHangState
        _playerControlMode.Target_SetVelocityZero();
        _playerControlMode.Target_SetRigidbodyDynamic();    // was static from ledge hang state
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}