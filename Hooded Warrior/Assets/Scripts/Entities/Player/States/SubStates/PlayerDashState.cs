using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool _isHolding;
    private bool _dashInputStop;
    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector3 _lastAIPosition;
    private float _lastDashTime;

    public PlayerDashState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _isHolding = true;
        _player.InputHandler.UseDashInput();
        _dashDirection = Vector2.right * _player.FacingDirection;

        Time.timeScale = _dataPlayer.HoldTimeScale;
        StartTime = Time.unscaledTime;

        _player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (_player.Rigidbody.velocity.y > 0f)
            _player.SetVelocityY(_player.Rigidbody.velocity.y * _dataPlayer.DashEndYMultiplier);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isAbilityDone)
            if (_isHolding)
            {
                _dashDirectionInput = _player.InputHandler.RawDashDirectionInput;
                _dashInputStop = _player.InputHandler.DashInputStop;

                if (_dashDirection != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                _player.DashDirectionIndicator.transform.rotation = Quaternion.Euler(0f, 0f, angle - 45);

                if (_dashInputStop || Time.unscaledTime >= StartTime + _dataPlayer.MaxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    _player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    _player.SetVelocity(_dataPlayer.DashVelocity, _dashDirection);
                    _player.Rigidbody.drag = _dataPlayer.Drag;
                    _player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                _player.SetVelocity(_dataPlayer.DashVelocity, _dashDirection);
                _player.Animator.SetFloat(PlayerControllerParameters.VelocityY, _player.Rigidbody.velocity.y);
                _player.Animator.SetFloat(PlayerControllerParameters.VelocityX, Mathf.Abs(_player.Rigidbody.velocity.x));

                CheckIfShouldPlaceAfterImage();

                if (Time.time >= StartTime + _dataPlayer.DashTime)
                {
                    _player.Rigidbody.drag = 0f;
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
