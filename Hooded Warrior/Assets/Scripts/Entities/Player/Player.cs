using System;
using UnityEngine;

public sealed class Player : Entity
{
    #region Components & Data
    [SerializeField] private PlayerExternComponents _playerExternComponents;
    [SerializeField] private PlayerData             _playerData;

    private PlayerActionComponents                  _playerIntStatusComponents;
    #endregion

    #region Component Getters
    public PlayerData                               Data            { get { return _playerData; } }
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

    protected override void InitializeStates()
    {
        AddNewState((int)PlayerStateID.Idle,            new PlayerIdleState(this, PlayerControllerParameters.Idle_b));
        AddNewState((int)PlayerStateID.Move,            new PlayerMoveState(this, PlayerControllerParameters.Move_b));
        AddNewState((int)PlayerStateID.Jump,            new PlayerJumpState(this, PlayerControllerParameters.InAir_b));
        AddNewState((int)PlayerStateID.InAir,           new PlayerInAirState(this, PlayerControllerParameters.InAir_b));
        AddNewState((int)PlayerStateID.Land,            new PlayerLandState(this, PlayerControllerParameters.Land_b));
        AddNewState((int)PlayerStateID.WallSlide,       new PlayerWallSlideState(this, PlayerControllerParameters.WallSlide_b));
        AddNewState((int)PlayerStateID.WallGrab,        new PlayerWallGrabState(this, PlayerControllerParameters.WallGrab_b));
        AddNewState((int)PlayerStateID.WallClimb,       new PlayerWallClimbState(this, PlayerControllerParameters.WallClimb_b));
        AddNewState((int)PlayerStateID.WallJump,        new PlayerWallJumpState(this, PlayerControllerParameters.InAir_b));
        AddNewState((int)PlayerStateID.LedgeClimb,      new PlayerLedgeClimbState(this, PlayerControllerParameters.LedgeClimbState_b));
        AddNewState((int)PlayerStateID.Dash,            new PlayerDashState(this, PlayerControllerParameters.InAir_b));
        AddNewState((int)PlayerStateID.CrouchIdle,      new PlayerCrouchIdleState(this, PlayerControllerParameters.CrouchIdle_b));
        AddNewState((int)PlayerStateID.CrouchMove,      new PlayerCrouchMoveState(this, PlayerControllerParameters.CrouchMove_b));
        AddNewState((int)PlayerStateID.Roll,            new PlayerRollState(this, PlayerControllerParameters.Roll_b));
        AddNewState((int)PlayerStateID.PrimaryAttack,   new PlayerAttackState(this, PlayerControllerParameters.Combat_b));
        AddNewState((int)PlayerStateID.SecondaryDefend, new PlayerDefendState(this, PlayerControllerParameters.Combat_b));
        AddNewState((int)PlayerStateID.SpellCast,       new PlayerSpellState(this, PlayerControllerParameters.Combat_b));

        //_primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
        //_secondaryDefendState.SetShield(_inventory.Shield);
        //_spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }
    #endregion
}

