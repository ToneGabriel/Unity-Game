using System;
using UnityEngine;

public sealed class Player : Entity
{
    #region Components & Data
    [SerializeField] private PlayerExternComponents _playerExternComponents;
    [SerializeField] private PlayerStateData        _playerStateData;

    private PlayerActionComponents                  _playerIntStatusComponents;
    #endregion

    #region Component Getters
    public PlayerStateData                          StateData       { get { return _playerStateData; } }
    public ref PlayerActionComponents               AdvancedStatus  { get { return ref _playerIntStatusComponents; } }
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
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        //ObjectPoolManager.Instance.RequestPool<PlayerAfterImage>();

        //gameObject.SetActive(false);                    // Allows "Awake" on application start but prevents loading errors
    }

    protected override void Update()
    {
        base.Update();

        SetAnimatorFloatParam(PlayerControllerParameters.VelocityY_f, VelocityY);
        SetAnimatorFloatParam(PlayerControllerParameters.VelocityX_f, Mathf.Abs(VelocityX));
    }
    #endregion

    #region Setters
    public void ResetAmountOfJumpsLeft()
    {
        _playerIntStatusComponents.AmountOfJumpsLeft = _playerStateData.MaxAmountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        --_playerIntStatusComponents.AmountOfJumpsLeft;
    }

    public void ResetAndDecreaseAmountOfJumpsLeft()
    {
        _playerIntStatusComponents.AmountOfJumpsLeft = _playerStateData.MaxAmountOfJumps - 1;
    }

    public void SetDashArrowActive(bool value)
    {
        _playerExternComponents._dashDirectionIndicator.SetActive(value);
    }

    public void SetDashArrowRotation(Quaternion rotation)
    {
        _playerExternComponents._dashDirectionIndicator.transform.rotation = rotation;
    }

    public void SetColiderHeight(float height)
    {
        Vector2 center = _entityActionComponents.BoxCollider.offset;
        _workspaceVector2.Set(_entityActionComponents.BoxCollider.size.x, height);

        center.y += (height - _entityActionComponents.BoxCollider.size.y) / 2;

        _entityActionComponents.BoxCollider.size   = _workspaceVector2;
        _entityActionComponents.BoxCollider.offset = center;
    }

    public GameObject GetLightOrbPosition()
    {
        return _playerExternComponents._lightOrbPosition;
    }

    public void SetLightOrbPosition(Vector2 position)
    {
        _playerExternComponents._lightOrbPosition.transform.localPosition = position;
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
        if (inputX != 0 && inputX != _entityActionComponents.FacingDirection)
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

        if (_entityActionComponents.IsDead)
        {
            gameObject.SetActive(false);

            Instantiate(_playerStateData.DeathBloodParticle,
                        transform.position,
                        _playerStateData.DeathBloodParticle.transform.rotation);

            Instantiate(_playerStateData.DeathChunkParticle,
                        transform.position,
                        _playerStateData.DeathChunkParticle.transform.rotation);
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

        _entityActionComponents.CurrentHealth = data.PlayerHealth;
        _entityActionComponents.FacingDirection = data.PlayerFacingDirection;
        transform.position = data.PlayerPosition.GetValues();
        transform.rotation = data.PlayerRotation.GetValues();
    }
    #endregion

    #region Other Functions
    public void SetNewGameData()
    {
        _entityActionComponents.FacingDirection = 1;
        _entityActionComponents.CurrentHealth = _entityData.MaxHealth;
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
        RaycastHit2D xHit = Physics2D.Raycast(  _entityExternComponents.EnvironmentCheck.transform.position,
                                                Vector2.right * _entityActionComponents.FacingDirection,
                                                _entityData.EnvironmentCheckDistance,
                                                _entityData.WhatIsGround);

        float xDistance = xHit.distance;
        _workspaceVector2.Set(xDistance * _entityActionComponents.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(  _entityExternComponents.LedgeCheck.transform.position + (Vector3)_workspaceVector2,
                                                Vector2.down,
                                                _entityExternComponents.LedgeCheck.transform.position.y - _entityExternComponents.EnvironmentCheck.transform.position.y,
                                                _entityData.WhatIsGround);

        float yDistance = yHit.distance;
        _workspaceVector2.Set(  _entityExternComponents.EnvironmentCheck.transform.position.x + xDistance * _entityActionComponents.FacingDirection,
                                _entityExternComponents.LedgeCheck.transform.position.y - yDistance);
        
        return _workspaceVector2;
    }

    private void AnimationTrigger()
    {
        //_stateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        //_stateMachine.CurrentState.AnimationFinishTrigger();
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
        //Gizmos.DrawLine(_ledgeCheck.transform.position, _ledgeCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
    }

    protected override void FSMInitializeModes()
    {
        AddNewMode(0);  // only 1 mode

        //AddNewState(0, (int)PlayerStateID.Idle,            new PlayerIdleState(this, PlayerControllerParameters.Idle_b));
        //AddNewState(0, (int)PlayerStateID.Move,            new PlayerMoveState(this, PlayerControllerParameters.Move_b));
        //AddNewState(0, (int)PlayerStateID.Jump,            new PlayerJumpState(this, PlayerControllerParameters.InAir_b));
        //AddNewState(0, (int)PlayerStateID.InAir,           new PlayerInAirState(this, PlayerControllerParameters.InAir_b));
        //AddNewState(0, (int)PlayerStateID.Land,            new PlayerLandState(this, PlayerControllerParameters.Land_b));
        //AddNewState(0, (int)PlayerStateID.WallSlide,       new PlayerWallSlideState(this, PlayerControllerParameters.WallSlide_b));
        //AddNewState(0, (int)PlayerStateID.WallGrab,        new PlayerWallGrabState(this, PlayerControllerParameters.WallGrab_b));
        //AddNewState(0, (int)PlayerStateID.WallClimb,       new PlayerWallClimbState(this, PlayerControllerParameters.WallClimb_b));
        //AddNewState(0, (int)PlayerStateID.WallJump,        new PlayerWallJumpState(this, PlayerControllerParameters.InAir_b));
        //AddNewState(0, (int)PlayerStateID.LedgeClimb,      new PlayerLedgeClimbState(this, PlayerControllerParameters.LedgeClimbState_b));
        //AddNewState(0, (int)PlayerStateID.Dash,            new PlayerDashState(this, PlayerControllerParameters.InAir_b));
        //AddNewState(0, (int)PlayerStateID.CrouchIdle,      new PlayerCrouchIdleState(this, PlayerControllerParameters.CrouchIdle_b));
        //AddNewState(0, (int)PlayerStateID.CrouchMove,      new PlayerCrouchMoveState(this, PlayerControllerParameters.CrouchMove_b));
        //AddNewState(0, (int)PlayerStateID.Roll,            new PlayerRollState(this, PlayerControllerParameters.Roll_b));
        //AddNewState(0, (int)PlayerStateID.PrimaryAttack,   new PlayerAttackState(this, PlayerControllerParameters.Combat_b));
        //AddNewState(0, (int)PlayerStateID.SecondaryDefend, new PlayerDefendState(this, PlayerControllerParameters.Combat_b));
        //AddNewState(0, (int)PlayerStateID.SpellCast,       new PlayerSpellState(this, PlayerControllerParameters.Combat_b));

        //_primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
        //_secondaryDefendState.SetShield(_inventory.Shield);
        //_spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    //protected override void FSMInitializeTransitions()
    //{
    //    // from Idle...
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.Jump,          () => { return InputManager.Instance.JumpInput && CanJump(); });
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.InAir,         () => { return !IsGrounded(); });
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.WallGrab,      () => { return InputManager.Instance.GrabInput && IsTouchingWall() && IsTouchingLedge(transform.right); });
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.Dash,          () => { return InputManager.Instance.DashInput && /*&& _player._dashState.CheckIfCanDash()*/ !IsTouchingCeiling(); });
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.Move,          () => { return InputManager.Instance.NormalizedInputX != 0; });
    //    AddNewTransition((int)PlayerStateID.Idle, (int)PlayerStateID.CrouchIdle,    () => { return InputManager.Instance.NormalizedInputY == -1; });

    //    // from Move...
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.Jump,          () => { return InputManager.Instance.JumpInput && CanJump(); });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.InAir,         () => { return !IsGrounded(); });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.WallGrab,      () => { return InputManager.Instance.GrabInput && IsTouchingWall() && IsTouchingLedge(transform.right); });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.Dash,          () => { return InputManager.Instance.DashInput && /*&& _player._dashState.CheckIfCanDash()*/ !IsTouchingCeiling(); });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.Idle,          () => { return InputManager.Instance.NormalizedInputX == 0; });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.CrouchMove,    () => { return InputManager.Instance.NormalizedInputY == -1; });
    //    AddNewTransition((int)PlayerStateID.Move, (int)PlayerStateID.Roll,          () => { return InputManager.Instance.RollInput && IsGrounded(); });

    //    // from Jump...
    //    // TODO: _isAbilityDone
    //    AddNewTransition((int)PlayerStateID.Jump, (int)PlayerStateID.CrouchIdle,    () => { return IsTouchingCeiling(); });
    //    AddNewTransition((int)PlayerStateID.Jump, (int)PlayerStateID.Idle,          () => { return IsGrounded() && VelocityY < 0.01f; });
    //    AddNewTransition((int)PlayerStateID.Jump, (int)PlayerStateID.InAir,         () => { return !IsGrounded(); });

    //    // from InAir...
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.Land,         () => { return IsGrounded() && VelocityY < 0.01f; });
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.LedgeClimb,   () => { return IsTouchingWall() && !IsTouchingLedge(transform.right) && !IsGrounded(); });
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.Jump,         () => { return InputManager.Instance.JumpInput && CanJump(); });
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.WallGrab,     () => { return InputManager.Instance.GrabInput && IsTouchingWall() && IsTouchingLedge(transform.right); });
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.WallSlide,    () => { return !InputManager.Instance.GrabInput && IsTouchingWall(); });
    //    AddNewTransition((int)PlayerStateID.InAir, (int)PlayerStateID.Dash,         () => { return InputManager.Instance.DashInput && /*&& _player._dashState.CheckIfCanDash()*/ !IsTouchingCeiling(); });

    //    // from Land...
    //    //AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.Jump, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.InAir, () => { return false; });
    //    //AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.WallGrab, () => { return false; });
    //    //AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.Dash, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.Move, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Land, (int)PlayerStateID.Idle, () => { return false; });

    //    // from WallSlide...
    //    AddNewTransition((int)PlayerStateID.WallSlide, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallSlide, (int)PlayerStateID.InAir, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallSlide, (int)PlayerStateID.LedgeClimb, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallSlide, (int)PlayerStateID.WallGrab, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallSlide, (int)PlayerStateID.WallJump, () => { return false; });

    //    // from WallGrab...
    //    AddNewTransition((int)PlayerStateID.WallGrab, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallGrab, (int)PlayerStateID.InAir, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallGrab, (int)PlayerStateID.LedgeClimb, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallGrab, (int)PlayerStateID.WallClimb, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallGrab, (int)PlayerStateID.WallSlide, () => { return false; });

    //    // from WallClimb...
    //    AddNewTransition((int)PlayerStateID.WallClimb, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallClimb, (int)PlayerStateID.InAir, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallClimb, (int)PlayerStateID.LedgeClimb, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallClimb, (int)PlayerStateID.WallGrab, () => { return false; });

    //    // from WallJump
    //    AddNewTransition((int)PlayerStateID.WallJump, (int)PlayerStateID.CrouchIdle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallJump, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.WallJump, (int)PlayerStateID.InAir, () => { return false; });

    //    // from LedgeClimb...
    //    AddNewTransition((int)PlayerStateID.LedgeClimb, (int)PlayerStateID.WallSlide, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.LedgeClimb, (int)PlayerStateID.WallJump, () => { return false; });
    //    // TODO: add climb

    //    // from Dash...
    //    AddNewTransition((int)PlayerStateID.Dash, (int)PlayerStateID.CrouchIdle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Dash, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Dash, (int)PlayerStateID.InAir, () => { return false; });

    //    // from CrouchIdle...
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.Jump, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.InAir, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.WallGrab, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.Dash, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.CrouchMove, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchIdle, (int)PlayerStateID.Idle, () => { return false; });

    //    // from CrouchMove...
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.Jump, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.InAir, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.WallGrab, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.Dash, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.CrouchIdle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.CrouchMove, (int)PlayerStateID.Move, () => { return false; });

    //    // from Roll...
    //    AddNewTransition((int)PlayerStateID.Roll, (int)PlayerStateID.CrouchIdle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Roll, (int)PlayerStateID.Idle, () => { return false; });
    //    AddNewTransition((int)PlayerStateID.Roll, (int)PlayerStateID.InAir, () => { return false; });

    //    // TODO: add more
    //}

    protected override bool FSMUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FSMFixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }
    #endregion
}

