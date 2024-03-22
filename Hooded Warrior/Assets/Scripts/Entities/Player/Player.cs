﻿using UnityEngine;

public sealed class Player : Entity
{
    #region Components & Data
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerInventory    _inventory;
    [SerializeField] private GameObject         _dashDirectionIndicator;
    [SerializeField] private GameObject         _lightOrbPosition;
    [SerializeField] private Data_Player        _dataPlayer;
    #endregion

    //#region States
    //private PlayerIdleState         _idleState;
    //private PlayerMoveState         _moveState;
    //private PlayerJumpState         _jumpState;
    //private PlayerInAirState        _inAirState;
    //private PlayerLandState         _landState;
    //private PlayerWallGrabState     _wallGrabState;
    //private PlayerWallSlideState    _wallSlideState;
    //private PlayerWallClimbState    _wallClimbState;
    //private PlayerWallJumpState     _wallJumpState;
    //private PlayerLedgeClimbState   _ledgeClimbState;
    //private PlayerDashState         _dashState;
    //private PlayerCrouchIdleState   _crouchIdleState;
    //private PlayerCrouchMoveState   _crouchMoveState;
    //private PlayerRollState         _rollState;

    //private PlayerAttackState       _primaryAttackState;
    //private PlayerDefendState       _secondaryDefendState;
    //private PlayerSpellState        _spellCastState;
    //#endregion

    #region Others
    private int _weaponIndex;
    private int _spellIndex;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        _inputHandler    = GetComponent<PlayerInputHandler>();
        _inventory       = GetComponent<PlayerInventory>();

        InitializeStates();

        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.InitializeState((int)PlayerStateID.Idle);
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
    private void InitializeStates()
    {
        _weaponIndex    = 0;
        _spellIndex     = 0;
        _stateMachine   = new FiniteStateMachine((int)PlayerStateID.Count);

        _stateMachine.AddNewState((int)PlayerStateID.Idle,             new PlayerIdleState(this, PlayerControllerParameters.Idle_b));
        _stateMachine.AddNewState((int)PlayerStateID.Move,             new PlayerMoveState(this, PlayerControllerParameters.Move_b));
        _stateMachine.AddNewState((int)PlayerStateID.Jump,             new PlayerJumpState(this, PlayerControllerParameters.InAir_b));
        _stateMachine.AddNewState((int)PlayerStateID.InAir,            new PlayerInAirState(this, PlayerControllerParameters.InAir_b));
        _stateMachine.AddNewState((int)PlayerStateID.Land,             new PlayerLandState(this, PlayerControllerParameters.Land_b));
        _stateMachine.AddNewState((int)PlayerStateID.WallSlide,        new PlayerWallSlideState(this, PlayerControllerParameters.WallSlide_b));
        _stateMachine.AddNewState((int)PlayerStateID.WallGrab,         new PlayerWallGrabState(this, PlayerControllerParameters.WallGrab_b));
        _stateMachine.AddNewState((int)PlayerStateID.WallClimb,        new PlayerWallClimbState(this, PlayerControllerParameters.WallClimb_b));
        _stateMachine.AddNewState((int)PlayerStateID.WallJump,         new PlayerWallJumpState(this, PlayerControllerParameters.InAir_b));
        _stateMachine.AddNewState((int)PlayerStateID.LedgeClimb,       new PlayerLedgeClimbState(this, PlayerControllerParameters.LedgeClimbState_b));
        _stateMachine.AddNewState((int)PlayerStateID.Dash,             new PlayerDashState(this, PlayerControllerParameters.InAir_b));
        _stateMachine.AddNewState((int)PlayerStateID.CrouchIdle,       new PlayerCrouchIdleState(this, PlayerControllerParameters.CrouchIdle_b));
        _stateMachine.AddNewState((int)PlayerStateID.CrouchMove,       new PlayerCrouchMoveState(this, PlayerControllerParameters.CrouchMove_b));
        _stateMachine.AddNewState((int)PlayerStateID.Roll,             new PlayerRollState(this, PlayerControllerParameters.Roll_b));
        _stateMachine.AddNewState((int)PlayerStateID.PrimaryAttack,    new PlayerAttackState(this, PlayerControllerParameters.Combat_b));
        _stateMachine.AddNewState((int)PlayerStateID.SecondaryDefend,  new PlayerDefendState(this, PlayerControllerParameters.Combat_b));
        _stateMachine.AddNewState((int)PlayerStateID.SpellCast,        new PlayerSpellState(this, PlayerControllerParameters.Combat_b));

        //_primaryAttackState.SetWeapon(_inventory.Weapons[_weaponIndex]);
        //_secondaryDefendState.SetShield(_inventory.Shield);
        //_spellCastState.SetSpell(_inventory.Spells[_spellIndex]);
    }

    public void SetColiderHeight(float height)
    {
        Vector2 center = _objectComponents.BoxCollider.offset;
        _workspaceVector2.Set(_objectComponents.BoxCollider.size.x, height);

        center.y += (height - _objectComponents.BoxCollider.size.y) / 2;

        _objectComponents.BoxCollider.size   = _workspaceVector2;
        _objectComponents.BoxCollider.offset = center;
    }

    public void SetLightOrbPosition(Vector2 position)
    {
        _lightOrbPosition.transform.localPosition = position;
    }
    #endregion

    #region Checkers
    public void CheckIfShouldFlip(int inputX)
    {
        if (inputX != 0 && inputX != _statusComponents.FacingDirection)
            Flip();
    }

    public bool CheckIfCanDefend()
    {
        return false;// !_secondaryDefendState.Shield.IsOnCooldown;
    }

    public bool CheckIfCanCastSpell()
    {
        return false;// !_spellCastState.Spell.IsOnCooldown;
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (_statusComponents.IsDead)
        {
            gameObject.SetActive(false);
            Instantiate(_dataPlayer.DeathBloodParticle, transform.position, _dataPlayer.DeathBloodParticle.transform.rotation);
            Instantiate(_dataPlayer.DeathChunkParticle, transform.position, _dataPlayer.DeathChunkParticle.transform.rotation);
        }
    }

    public override bool CanTakeDamage()
    {
        return true;// (!_secondaryDefendState.IsHolding || _statusComponents.LastDamageDirection != _statusComponents.FacingDirection);
    }

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        InterruptActions();
        DamageHop(_objectComponents.DataEntity.DamageHopDirection, _objectComponents.DataEntity.DamageHopSpeed);

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

        _statusComponents.CurrentHealth = data.PlayerHealth;
        _statusComponents.FacingDirection = data.PlayerFacingDirection;
        transform.position = data.PlayerPosition.GetValues();
        transform.rotation = data.PlayerRotation.GetValues();
    }
    #endregion

    #region Other Functions
    public void SetNewGameData()
    {
        _statusComponents.FacingDirection = 1;
        _statusComponents.CurrentHealth = _objectComponents.DataEntity.MaxHealth;
        transform.position = GameManager.Instance.GameStartPlayerPosition.position;
        transform.rotation = GameManager.Instance.GameStartPlayerPosition.rotation;
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
        RaycastHit2D xHit = Physics2D.Raycast(  _objectComponents.EnvironmentCheck.transform.position,
                                                Vector2.right * _statusComponents.FacingDirection,
                                                _objectComponents.DataEntity.EnvironmentCheckDistance,
                                                _objectComponents.DataEntity.WhatIsGround);

        float xDistance = xHit.distance;
        _workspaceVector2.Set(xDistance * _statusComponents.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(  _objectComponents.LedgeCheck.transform.position + (Vector3)_workspaceVector2,
                                                Vector2.down,
                                                _objectComponents.LedgeCheck.transform.position.y - _objectComponents.EnvironmentCheck.transform.position.y,
                                                _objectComponents.DataEntity.WhatIsGround);

        float yDistance = yHit.distance;
        _workspaceVector2.Set(  _objectComponents.EnvironmentCheck.transform.position.x + xDistance * _statusComponents.FacingDirection,
                                _objectComponents.LedgeCheck.transform.position.y - yDistance);
        
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
