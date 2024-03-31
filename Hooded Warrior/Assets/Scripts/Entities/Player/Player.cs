using System;
using UnityEngine;

public sealed class Player : Entity
{
    #region Components & Data
    [SerializeField]
    private PlayerExternalObjectComponents _playerExtObjComponents;
    private PlayerInternalStatusComponents _playerIntStatusComponents;

    [SerializeField]
    private PlayerData _playerData;
    #endregion

    #region Component Getters
    public ref PlayerInternalStatusComponents   AdvancedStatus  { get { return ref _playerIntStatusComponents; } }
    public PlayerData                           PlayerData      { get { return _playerData; } }
    #endregion

    #region Others
    //private int _weaponIndex;
    //private int _spellIndex;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        //_playerExtObjComponents._inventory = GetComponent<PlayerInventory>();
        //_weaponIndex    = 0;
        //_spellIndex     = 0;
        
        InitializeStates();
        InitializeCoditions();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.InitializeState(_states[(int)PlayerStateID.Idle]);
    }

    protected override void Start()
    {
        base.Start();

        //ObjectPoolManager.Instance.RequestPool<PlayerAfterImage>();

        //gameObject.SetActive(false);                    // Allows "Awake" on application start but prevents loading errors
    }
    #endregion

    #region Setters
    public void ResetAmountOfJumpsLeft()
    {
        _playerIntStatusComponents.AmountOfJumpsLeft = _playerData.MaxAmountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        --_playerIntStatusComponents.AmountOfJumpsLeft;
    }

    public void ResetAndDecreaseAmountOfJumpsLeft()
    {
        _playerIntStatusComponents.AmountOfJumpsLeft = _playerData.MaxAmountOfJumps - 1;
    }

    public void SetDashArrowActive(bool value)
    {
        _playerExtObjComponents._dashDirectionIndicator.SetActive(value);
    }

    public void SetDashArrowRotation(Quaternion rotation)
    {
        _playerExtObjComponents._dashDirectionIndicator.transform.rotation = rotation;
    }

    public void SetColiderHeight(float height)
    {
        Vector2 center = _entityIntObjComponents.BoxCollider.offset;
        _workspaceVector2.Set(_entityIntObjComponents.BoxCollider.size.x, height);

        center.y += (height - _entityIntObjComponents.BoxCollider.size.y) / 2;

        _entityIntObjComponents.BoxCollider.size   = _workspaceVector2;
        _entityIntObjComponents.BoxCollider.offset = center;
    }

    public GameObject GetLightOrbPosition()
    {
        return _playerExtObjComponents._lightOrbPosition;
    }

    public void SetLightOrbPosition(Vector2 position)
    {
        _playerExtObjComponents._lightOrbPosition.transform.localPosition = position;
    }
    #endregion

    #region Checkers
    public bool CanJump()
    {
        return (_playerIntStatusComponents.AmountOfJumpsLeft > 0);
    }

    public bool CanDash()
    {
        return false;   // TODO
    }

    public void FlipIfShould(int inputX)
    {
        if (inputX != 0 && inputX != _entityIntStatusComponents.FacingDirection)
            Flip();
    }

    public bool CanDefend()
    {
        return false;// !_secondaryDefendState.Shield.IsOnCooldown;
    }

    public bool CanCastSpell()
    {
        return false;// !_spellCastState.Spell.IsOnCooldown;
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (_entityIntStatusComponents.IsDead)
        {
            gameObject.SetActive(false);

            Instantiate(_playerData.DeathBloodParticle,
                        transform.position,
                        _playerData.DeathBloodParticle.transform.rotation);

            Instantiate(_playerData.DeathChunkParticle,
                        transform.position,
                        _playerData.DeathChunkParticle.transform.rotation);
        }
    }

    public override bool CanTakeDamage()
    {
        return true;// (!_secondaryDefendState.IsHolding || _statusComponents.LastDamageDirection != _statusComponents.FacingDirection);
    }

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        InterruptActions();
        DamageHop(_entityData.DamageHopDirection, _entityData.DamageHopSpeed);

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

        _entityIntStatusComponents.CurrentHealth = data.PlayerHealth;
        _entityIntStatusComponents.FacingDirection = data.PlayerFacingDirection;
        transform.position = data.PlayerPosition.GetValues();
        transform.rotation = data.PlayerRotation.GetValues();
    }
    #endregion

    #region Other Functions
    private void InitializeStates()
    {
        _stateMachine   = new FiniteStateMachine();
        _states         = new State[(int)PlayerStateID.Count];

        _states[(int)PlayerStateID.Idle]            = new PlayerIdleState(this, PlayerControllerParameters.Idle_b);
        _states[(int)PlayerStateID.Move]            = new PlayerMoveState(this, PlayerControllerParameters.Move_b);
        _states[(int)PlayerStateID.Jump]            = new PlayerJumpState(this, PlayerControllerParameters.InAir_b);
        _states[(int)PlayerStateID.InAir]           = new PlayerInAirState(this, PlayerControllerParameters.InAir_b);
        _states[(int)PlayerStateID.Land]            = new PlayerLandState(this, PlayerControllerParameters.Land_b);
        _states[(int)PlayerStateID.WallSlide]       = new PlayerWallSlideState(this, PlayerControllerParameters.WallSlide_b);
        _states[(int)PlayerStateID.WallGrab]        = new PlayerWallGrabState(this, PlayerControllerParameters.WallGrab_b);
        _states[(int)PlayerStateID.WallClimb]       = new PlayerWallClimbState(this, PlayerControllerParameters.WallClimb_b);
        _states[(int)PlayerStateID.WallJump]        = new PlayerWallJumpState(this, PlayerControllerParameters.InAir_b);
        _states[(int)PlayerStateID.LedgeClimb]      = new PlayerLedgeClimbState(this, PlayerControllerParameters.LedgeClimbState_b);
        _states[(int)PlayerStateID.Dash]            = new PlayerDashState(this, PlayerControllerParameters.InAir_b);
        _states[(int)PlayerStateID.CrouchIdle]      = new PlayerCrouchIdleState(this, PlayerControllerParameters.CrouchIdle_b);
        _states[(int)PlayerStateID.CrouchMove]      = new PlayerCrouchMoveState(this, PlayerControllerParameters.CrouchMove_b);
        _states[(int)PlayerStateID.Roll]            = new PlayerRollState(this, PlayerControllerParameters.Roll_b);
        _states[(int)PlayerStateID.PrimaryAttack]   = new PlayerAttackState(this, PlayerControllerParameters.Combat_b);
        _states[(int)PlayerStateID.SecondaryDefend] = new PlayerDefendState(this, PlayerControllerParameters.Combat_b);
        _states[(int)PlayerStateID.SpellCast]       = new PlayerSpellState(this, PlayerControllerParameters.Combat_b);

        //_primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
        //_secondaryDefendState.SetShield(_inventory.Shield);
        //_spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    private void InitializeCoditions()
    {
        _conditions = new Func<bool>[(int)PlayerStateTransitionID.Count];

        _conditions[(int)PlayerStateTransitionID.GroundedToJump]            = () => { return (InputManager.Instance.JumpInput && CanJump()); };
        _conditions[(int)PlayerStateTransitionID.GroundedToInAir]           = () => { return (!IsGrounded()); };
        _conditions[(int)PlayerStateTransitionID.GroundedToWallGrab]        = () => { return (InputManager.Instance.GrabInput && IsTouchingWall() && IsTouchingLedge(transform.right)); };
        _conditions[(int)PlayerStateTransitionID.GroundedToDash]            = () => { return (InputManager.Instance.DashInput && !IsTouchingCeiling()); };
        
        _conditions[(int)PlayerStateTransitionID.AbilityToCrouchIdle]       = () => { return (IsTouchingCeiling()); };
        _conditions[(int)PlayerStateTransitionID.AbilityToIdle]             = () => { return (IsGrounded() && RBVelocityY < 0.01f); };
        _conditions[(int)PlayerStateTransitionID.AbilityToInAir]            = () => { return true; };
        
        _conditions[(int)PlayerStateTransitionID.TouchingWallToIdle]        = () => { return (!InputManager.Instance.GrabInput && IsGrounded()); };
        _conditions[(int)PlayerStateTransitionID.TouchingWallToInAir]       = () => { return (!IsTouchingWall()); };
        _conditions[(int)PlayerStateTransitionID.TouchingWallToLedgeClimb]  = () => { return (IsTouchingWall() && !IsTouchingLedge(transform.right)); };
        
        _conditions[(int)PlayerStateTransitionID.IdleToMove]                = () => { return (InputManager.Instance.NormalizedInputX != 0); };
        _conditions[(int)PlayerStateTransitionID.IdleToCrouchIdle]          = () => { return (InputManager.Instance.NormalizedInputY == -1); };
        
        _conditions[(int)PlayerStateTransitionID.MoveToIdle]                = () => { return (InputManager.Instance.NormalizedInputX == 0); };
        _conditions[(int)PlayerStateTransitionID.MoveToCrouchMove]          = () => { return (InputManager.Instance.NormalizedInputY == -1); };
        _conditions[(int)PlayerStateTransitionID.MoveToRoll]                = () => { return (InputManager.Instance.RollInput && IsGrounded()); };
        
        _conditions[(int)PlayerStateTransitionID.InAirToLand]               = () => { return (IsGrounded() && RBVelocityY < 0.01f); };
        _conditions[(int)PlayerStateTransitionID.InAirToLedgeClimb]         = () => { return (IsTouchingWall() && !IsTouchingLedge(transform.right) && !IsGrounded()); };
        _conditions[(int)PlayerStateTransitionID.InAirToJump]               = () => { return (InputManager.Instance.JumpInput && CanJump()); };
        _conditions[(int)PlayerStateTransitionID.InAirToWallGrab]           = () => { return (InputManager.Instance.GrabInput && IsTouchingWall() && IsTouchingLedge(transform.right)); };
        _conditions[(int)PlayerStateTransitionID.InAirToWallSlide]          = () => { return (!InputManager.Instance.GrabInput && IsTouchingWall()); };
        _conditions[(int)PlayerStateTransitionID.InAirToDash]               = () => { return (InputManager.Instance.DashInput /*&& _player._dashState.CheckIfCanDash()*/); };
        
        //_conditions[(int)PlayerStateTransitionID.LandToIdle]                = () => { return _isStateAnimationFinished; };
        _conditions[(int)PlayerStateTransitionID.LandToMove]                = () => { return (InputManager.Instance.NormalizedInputX != 0); };
        
        _conditions[(int)PlayerStateTransitionID.WallSlideToWallGrab]       = () => { return (InputManager.Instance.GrabInput && InputManager.Instance.NormalizedInputY == 0); };
        _conditions[(int)PlayerStateTransitionID.WallSlideToWallJump]       = () => { return (InputManager.Instance.JumpInput && IsTouchingWall()); };
        _conditions[(int)PlayerStateTransitionID.WallSlideToInAir]          = () => { return (!InputManager.Instance.GrabInput && InputManager.Instance.NormalizedInputX != 0 && InputManager.Instance.NormalizedInputX != GeneralStatus.FacingDirection); };
        
        _conditions[(int)PlayerStateTransitionID.WallGrabToWallClimb]       = () => { return (InputManager.Instance.NormalizedInputY > 0); };
        _conditions[(int)PlayerStateTransitionID.WallGrabToWallSlide]       = () => { return (InputManager.Instance.NormalizedInputY < 0 || !InputManager.Instance.GrabInput); };
        
        _conditions[(int)PlayerStateTransitionID.WallClimbToWallGrab]       = () => { return (InputManager.Instance.NormalizedInputY != 1); };
        
        //_conditions[(int)PlayerStateTransitionID.LedgeClimbToWallSlide]     = () => { return (InputManager.Instance.NormalizedInputY == -1 && _isHanging && !_isClimbing); };
        //_conditions[(int)PlayerStateTransitionID.LedgeClimbToWallJump]      = () => { return (InputManager.Instance.JumpInput && !_isClimbing); };
        
        _conditions[(int)PlayerStateTransitionID.CrouchIdleToCrouchMove]    = () => { return (InputManager.Instance.NormalizedInputX != 0); };
        _conditions[(int)PlayerStateTransitionID.CrouchIdleToIdle]          = () => { return (InputManager.Instance.NormalizedInputY != -1 && !IsTouchingCeiling()); };
        
        _conditions[(int)PlayerStateTransitionID.CrouchMoveToCrouchIdle]    = () => { return (InputManager.Instance.NormalizedInputX == 0); };
        _conditions[(int)PlayerStateTransitionID.CrouchMoveToMove]          = () => { return (InputManager.Instance.NormalizedInputY != -1 && !IsTouchingCeiling()); };
    }

    public void SetNewGameData()
    {
        _entityIntStatusComponents.FacingDirection = 1;
        _entityIntStatusComponents.CurrentHealth = _entityData.MaxHealth;
        transform.SetPositionAndRotation(   GameManager.Instance.GameStartPlayerPosition.position,
                                            GameManager.Instance.GameStartPlayerPosition.rotation);
    }

    public void ChangeWeapon()
    {
        //if (_weaponIndex < _inventory.Weapons.Length - 1)
        //    _weaponIndex++;
        //else
        //    _weaponIndex = 0;
        //_primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
    }

    public void ChangeSpell()
    {
        //if (_spellIndex < _inventory.Spells.Length - 1)
        //    _spellIndex++;
        //else
        //    _spellIndex = 0;
        //_spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    private void InterruptActions()
    {
        //// cancel spell
        //if (_stateMachine.CurrentState == _spellCastState)
        //    _inventory.Spells[_spellIndex].AnimationFinishTrigger();
        //// cancel attack
        //else if (_stateMachine.CurrentState == _primaryAttackState)
        //    _inventory.Weapons[_weaponIndex].AnimationFinishTrigger();
        //// cancel defend
        //else if (_stateMachine.CurrentState == _secondaryDefendState)
        //    _inventory.Shield.AnimationFinishTrigger(); 
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(  _entityExtObjComponents.EnvironmentCheck.transform.position,
                                                Vector2.right * _entityIntStatusComponents.FacingDirection,
                                                _entityData.EnvironmentCheckDistance,
                                                _entityData.WhatIsGround);

        float xDistance = xHit.distance;
        _workspaceVector2.Set(xDistance * _entityIntStatusComponents.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(  _entityExtObjComponents.LedgeCheck.transform.position + (Vector3)_workspaceVector2,
                                                Vector2.down,
                                                _entityExtObjComponents.LedgeCheck.transform.position.y - _entityExtObjComponents.EnvironmentCheck.transform.position.y,
                                                _entityData.WhatIsGround);

        float yDistance = yHit.distance;
        _workspaceVector2.Set(  _entityExtObjComponents.EnvironmentCheck.transform.position.x + xDistance * _entityIntStatusComponents.FacingDirection,
                                _entityExtObjComponents.LedgeCheck.transform.position.y - yDistance);
        
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

