using UnityEngine;

public sealed partial class Player : Entity
{
    #region State Declarations
    private abstract partial class PlayerState              : EntityState { }

    private abstract partial class PlayerAbilityState       : PlayerState { }
    private abstract partial class PlayerGroundedState      : PlayerState { }
    private abstract partial class PlayerTouchingWallState  : PlayerState { }

    private sealed partial class PlayerIdleState            : PlayerGroundedState { }
    private sealed partial class PlayerMoveState            : PlayerGroundedState { }
    private sealed partial class PlayerJumpState            : PlayerAbilityState { }
    private sealed partial class PlayerInAirState           : PlayerState { }
    private sealed partial class PlayerLandState            : PlayerGroundedState { }
    private sealed partial class PlayerWallGrabState        : PlayerTouchingWallState { }
    private sealed partial class PlayerWallSlideState       : PlayerTouchingWallState { }
    private sealed partial class PlayerWallClimbState       : PlayerTouchingWallState { }
    private sealed partial class PlayerWallJumpState        : PlayerAbilityState { }
    private sealed partial class PlayerLedgeClimbState      : PlayerState { }
    private sealed partial class PlayerDashState            : PlayerAbilityState { }
    private sealed partial class PlayerCrouchIdleState      : PlayerGroundedState { }
    private sealed partial class PlayerCrouchMoveState      : PlayerGroundedState { }
    private sealed partial class PlayerRollState            : PlayerAbilityState { }

    private sealed partial class PlayerAttackState          : PlayerAbilityState { }
    private sealed partial class PlayerDefendState          : PlayerAbilityState { }
    private sealed partial class PlayerSpellState           : PlayerAbilityState { }
    #endregion State Declarations

    #region Components & Data
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerInventory    _inventory;
    [SerializeField] private GameObject         _dashDirectionIndicator;
    [SerializeField] private GameObject         _lightOrbPosition;
    [SerializeField] private Data_Player        _dataPlayer;
    #endregion

    #region States
    private PlayerIdleState         _idleState;
    private PlayerMoveState         _moveState;
    private PlayerJumpState         _jumpState;
    private PlayerInAirState        _inAirState;
    private PlayerLandState         _landState;
    private PlayerWallGrabState     _wallGrabState;
    private PlayerWallSlideState    _wallSlideState;
    private PlayerWallClimbState    _wallClimbState;
    private PlayerWallJumpState     _wallJumpState;
    private PlayerLedgeClimbState   _ledgeClimbState;
    private PlayerDashState         _dashState;
    private PlayerCrouchIdleState   _crouchIdleState;
    private PlayerCrouchMoveState   _crouchMoveState;
    private PlayerRollState         _rollState;

    private PlayerAttackState       _primaryAttackState;
    private PlayerDefendState       _secondaryDefendState;
    private PlayerSpellState        _spellCastState;
    #endregion

    #region Others
    private int _weaponIndex;
    private int _spellIndex;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        _inputHandler    = GetComponent<PlayerInputHandler>();
        _inventory       = GetComponent<PlayerInventory>();

        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.Initialize(_idleState);
    }

    protected override void Start()
    {
        base.Start();

        ObjectPoolManager.Instance.RequestPool<PlayerAfterImage>();

        gameObject.SetActive(false);                    // Allows "Awake" on application start but prevents loading errors
    }

    protected override void LogicUpdate()
    {
        base.LogicUpdate();

        _stateMachine.CurrentState.LogicUpdate();
    }

    protected override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Setters
    protected override void InitializeStates()
    {
        base.InitializeStates();

        _idleState              = new PlayerIdleState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Idle_b);
        _moveState              = new PlayerMoveState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Move_b);
        _jumpState              = new PlayerJumpState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        _inAirState             = new PlayerInAirState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        _landState              = new PlayerLandState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Land_b);
        _wallSlideState         = new PlayerWallSlideState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.WallSlide_b);
        _wallGrabState          = new PlayerWallGrabState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.WallGrab_b);
        _wallClimbState         = new PlayerWallClimbState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.WallClimb_b);
        _wallJumpState          = new PlayerWallJumpState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        _ledgeClimbState        = new PlayerLedgeClimbState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.LedgeClimbState_b);
        _dashState              = new PlayerDashState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        _crouchIdleState        = new PlayerCrouchIdleState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.CrouchIdle_b);
        _crouchMoveState        = new PlayerCrouchMoveState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.CrouchMove_b);
        _rollState              = new PlayerRollState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Roll_b);

        _primaryAttackState     = new PlayerAttackState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);
        _secondaryDefendState   = new PlayerDefendState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);
        _spellCastState         = new PlayerSpellState(this, _stateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);

        _weaponIndex            = 0;
        _spellIndex             = 0;

        _primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
        _secondaryDefendState.SetShield(_inventory.Shield);
        _spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    public void SetColiderHeight(float height)
    {
        Vector2 center = _boxCollider.offset;
        _workspaceVector2.Set(_boxCollider.size.x, height);

        center.y += (height - _boxCollider.size.y) / 2;

        _boxCollider.size   = _workspaceVector2;
        _boxCollider.offset = center;
    }

    public void SetLightOrbPosition(Vector2 position)
    {
        _lightOrbPosition.transform.localPosition = position;
    }
    #endregion

    #region Checkers
    public void CheckIfShouldFlip(int inputX)
    {
        if (inputX != 0 && inputX != FacingDirection)
            Flip();
    }

    public bool CheckIfCanDefend()
    {
        return !_secondaryDefendState.Shield.IsOnCooldown;
    }

    public bool CheckIfCanCastSpell()
    {
        return !_spellCastState.Spell.IsOnCooldown;
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (IsDead)
        {
            gameObject.SetActive(false);
            Instantiate(_dataPlayer.DeathBloodParticle, transform.position, _dataPlayer.DeathBloodParticle.transform.rotation);
            Instantiate(_dataPlayer.DeathChunkParticle, transform.position, _dataPlayer.DeathChunkParticle.transform.rotation);
        }
    }

    public override bool CanTakeDamage()
    {
        return (!_secondaryDefendState.IsHolding || LastDamageDirection != FacingDirection);
    }

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        InterruptActions();
        DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);

        base.AdditionalDamageActions(attackDetails);
    }

    public override void CheckStatus() => base.CheckStatus();
    #endregion

    #region Save Functions
    public override object CaptureState()
    {
        return new PlayerSaveData(this);
    }

    public override void RestoreState(ref object state)
    {
        var data = (PlayerSaveData)state;

        CurrentHealth = data.PlayerHealth;
        FacingDirection = data.PlayerFacingDirection;
        transform.position = data.PlayerPosition.GetValues();
        transform.rotation = data.PlayerRotation.GetValues();
    }
    #endregion

    #region Other Functions
    public void SetNewGameData()
    {
        FacingDirection = 1;
        CurrentHealth = _dataEntity.MaxHealth;
        transform.position = GameManager.Instance.GameStartPlayerPosition.position;
        transform.rotation = GameManager.Instance.GameStartPlayerPosition.rotation;
    }

    public void ChangeWeapon()
    {
        if (_weaponIndex < _inventory.Weapons.Length - 1)
            _weaponIndex++;
        else
            _weaponIndex = 0;
        _primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
    }

    public void ChangeSpell()
    {
        if (_spellIndex < _inventory.Spells.Length - 1)
            _spellIndex++;
        else
            _spellIndex = 0;
        _spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    private void InterruptActions()
    {
        // cancel spell
        if (_stateMachine.CurrentState == _spellCastState)
            _inventory.Spells[_spellIndex].AnimationFinishTrigger();
        // cancel attack
        else if (_stateMachine.CurrentState == _primaryAttackState)
            _inventory.Weapons[_weaponIndex].AnimationFinishTrigger();
        // cancel defend
        else if (_stateMachine.CurrentState == _secondaryDefendState)
            _inventory.Shield.AnimationFinishTrigger(); 
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_environmentCheck.transform.position, Vector2.right * FacingDirection, _dataEntity.EnvironmentCheckDistance, _dataEntity.WhatIsGround);
        float xDistance = xHit.distance;
        _workspaceVector2.Set(xDistance * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.transform.position + (Vector3)_workspaceVector2, Vector2.down, _ledgeCheck.transform.position.y - _environmentCheck.transform.position.y, _dataEntity.WhatIsGround);
        float yDistance = yHit.distance;
        _workspaceVector2.Set(_environmentCheck.transform.position.x + xDistance * FacingDirection, _ledgeCheck.transform.position.y - yDistance);
        
        return _workspaceVector2;
    }

    private void AnimationTrigger()
    {
        _stateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        _stateMachine.CurrentState.AnimationFinishTrigger();
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
        //Gizmos.DrawLine(_ledgeCheck.transform.position, _ledgeCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
    }
    #endregion
}

