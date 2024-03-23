﻿using UnityEngine;

public sealed class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool _isHolding;
    private bool _dashInputStop;
    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector3 _lastAIPosition;
    private float _lastDashTime;

    public PlayerDashState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _isHolding = true;
        InputManager.Instance.UseDashInput();
        _dashDirection = Vector2.right * _player.StatusComponents.FacingDirection;

        Time.timeScale = _dataPlayer.HoldTimeScale;
        _player.StatusComponents.StateStartTime = Time.unscaledTime;

        _player._dashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (_player.ObjectComponents.Rigidbody.velocity.y > 0f)
            _player.SetVelocityY(_player.ObjectComponents.Rigidbody.velocity.y * _dataPlayer.DashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isAbilityDone)
            if (_isHolding)
            {
                _dashDirectionInput = InputManager.Instance.DashDirectionInput;
                _dashInputStop = _player._inputHandler.DashInputStop;

                if (_dashDirection != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                _player._dashDirectionIndicator.transform.rotation = Quaternion.Euler(0f, 0f, angle - 45);

                if (_dashInputStop || Time.unscaledTime >= _player.StatusComponents.StateStartTime + _dataPlayer.MaxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    _player.StatusComponents.StateStartTime = Time.time;
                    _player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    _player.SetVelocity(_dataPlayer.DashVelocity, _dashDirection);
                    _player.ObjectComponents.Rigidbody.drag = _dataPlayer.Drag;
                    _player._dashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                _player.SetVelocity(_dataPlayer.DashVelocity, _dashDirection);
                _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player.ObjectComponents.Rigidbody.velocity.y);
                _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player.ObjectComponents.Rigidbody.velocity.x));

                CheckIfShouldPlaceAfterImage();

                if (Time.time >= _player.StatusComponents.StateStartTime + _dataPlayer.DashTime)
                {
                    _player.ObjectComponents.Rigidbody.drag = 0f;
                    _isAbilityDone = true;
                    _lastDashTime = Time.time;
                }
            }
    }

    private void PlaceAfterImage()
    {
        ObjectPoolManager.Instance.GetFromPool<PlayerAfterImage>(_player.transform.position, _player.transform.rotation);
        _lastAIPosition = _player.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(_player.transform.position, _lastAIPosition) >= _dataPlayer.DistanceBetweenAfterimages)
            PlaceAfterImage();
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + _dataPlayer.DashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }
}