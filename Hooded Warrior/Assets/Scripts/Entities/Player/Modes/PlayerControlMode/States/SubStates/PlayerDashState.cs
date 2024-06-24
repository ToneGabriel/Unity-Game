using UnityEngine;

public sealed class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool    _isHolding;
    private bool    _dashInputStop;
    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector3 _lastAIPosition;
    private float   _lastDashTime;

    public PlayerDashState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _isHolding = true;
        InputManager.Instance.UseDashInput();
        //_dashDirection = Vector2.right * _player.FacingDirection;

        Helpers.ChangeTimeScale(TimeScale.Frozen);
        //_stateStartTime = Time.unscaledTime;

        //_player.SetDashArrowActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        //if (_player.VelocityY > 0f)
        //    _player.SetVelocityY(_player.VelocityY * _player.StateData.DashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isAbilityDone)
            if (_isHolding)
            {
                _dashDirectionInput = InputManager.Instance.DashDirectionInput;
                // TODO
                //_dashInputStop = _player._inputHandler.DashInputStop;

                _dashDirection = _dashDirectionInput;
                _dashDirection.Normalize();

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                //_player.SetDashArrowRotation(Quaternion.Euler(0f, 0f, angle - 45));

                //if (_dashInputStop || Time.unscaledTime >= _stateStartTime + _player.StateData.MaxHoldTime)
                //{
                //    Helpers.ChangeTimeScale(TimeScale.Normal);
                //    _isHolding      = false;
                //    _stateStartTime = Time.time;
                //    _player.Drag    = _player.StateData.DashDrag;
                //    _player.FlipIfShould(Mathf.RoundToInt(_dashDirection.x));
                //    _player.SetDashArrowActive(false);
                //    _player.SetVelocity(_player.PlayerData.DashVelocity, _dashDirection);
                //    PlaceAfterImage();
                //}
            }
            else
            {
                //_player.SetVelocity(_player.StateData.DashVelocity, _dashDirection);

                //if (Vector2.Distance(_player.transform.position, _lastAIPosition) >= _player.StateData.DistanceBetweenAfterimages)
                //    PlaceAfterImage();

                //if (Time.time >= _stateStartTime + _player.StateData.DashTime)
                //{
                //    //_player.Drag    = 0f;
                //    _isAbilityDone  = true;
                //    _lastDashTime   = Time.time;
                //}
            }
    }

    private void PlaceAfterImage()
    {
        //ObjectPoolManager.Instance.GetFromPool<PlayerAfterImage>(_player.transform.position, _player.transform.rotation);
        //_lastAIPosition = _player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return false;
        //return CanDash && Time.time >= _lastDashTime + _player.StateData.DashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }
}