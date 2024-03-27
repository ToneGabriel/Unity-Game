﻿
public sealed class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
        _player.SetColiderHeight(_player.PlayerData.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.PlayerData.CrouchLightOrbPosition);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetColiderHeight(_player.PlayerData.StandColliderHeight);
        _player.SetLightOrbPosition(_player.PlayerData.StandLightOrbPosition);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _player.ChangeState((int)PlayerStateID.CrouchMove);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.Idle);
    }
}