using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : Entity
{
    private enum StateID
    {
        Control,
        Hit
    }

    private FiniteStateMachine<StateID> _playerController = null;

    #region Components & Data
    [SerializeField] private PlayerExternComponents _playerExternComponents;

    [Header("Player Modes Data")]
    [SerializeField] private PlayerControlModeData  _playerControlModeData;

    public int      JumpCount { get; set; }
    public Vector2  FuturePosition { get; set; }
    #endregion

    #region Others
    //private int _weaponIndex;
    //private int _spellIndex;
    #endregion

    #region Control Interface
    protected override State GetControlState()
    {
        return _playerController;
    }

    protected override bool UpdateConditions()
    {
        return true;
        //return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FixedUpdateConditions()
    {
        return true;
        //return !GameManager.Instance.IsGamePaused;
    }

    public override void ChangeState()
    {
        _playerController.ChangeState(StateID.Control);
    }
    #endregion Control Interface

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        _playerController = new FiniteStateMachine<StateID>();

        _playerController.InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Control, new PlayerControlMode(this, _playerControlModeData))
            //new KeyValuePair<StateID, State>(StateID.Hit,       null)
        );    

        _playerController.SetDefaultState(StateID.Control);

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

        //SetAnimatorFloatParam(PlayerControllerParameters.VelocityY_f, VelocityY);
        //SetAnimatorFloatParam(PlayerControllerParameters.VelocityX_f, Mathf.Abs(VelocityX));
    }
    #endregion

    #region Setters
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
        //Vector2 center = EntityInternComponents.BoxCollider.offset;
        //_workspaceVector2.Set(EntityInternComponents.BoxCollider.size.x, height);

        //center.y += (height - EntityInternComponents.BoxCollider.size.y) / 2;

        //EntityInternComponents.BoxCollider.size   = _workspaceVector2;
        //EntityInternComponents.BoxCollider.offset = center;
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
    public bool CanDash()
    {
        return false;   // TODO
    }

    public void FlipIfShould(int inputX)
    {
        //if (inputX != 0 && inputX != EntityInternComponents.FacingDirection)
        //    Flip();
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

        //if (EntityInternComponents.IsDead)
        //{
        //    gameObject.SetActive(false);

        //    Instantiate(_playerStateData.DeathBloodParticle,
        //                transform.position,
        //                _playerStateData.DeathBloodParticle.transform.rotation);

        //    Instantiate(_playerStateData.DeathChunkParticle,
        //                transform.position,
        //                _playerStateData.DeathChunkParticle.transform.rotation);
        //}
    }

    public override bool CanTakeDamage()
    {
        return true;// (!_secondaryDefendState.IsHolding || _statusComponents.LastDamageDirection != _statusComponents.FacingDirection);
    }

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        InterruptActions();
        //DamageHop(_data.DamageHopDirection, _data.DamageHopSpeed);

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

        //EntityInternComponents.CurrentHealth = data.PlayerHealth;
        //EntityInternComponents.FacingDirection = data.PlayerFacingDirection;
        transform.position = data.PlayerPosition.GetValues();
        transform.rotation = data.PlayerRotation.GetValues();
    }
    #endregion

    #region Other Functions
    public void SetNewGameData()
    {
        //EntityInternComponents.FacingDirection = 1;
        //EntityInternComponents.CurrentHealth = _data.MaxHealth;
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
    #endregion
}

