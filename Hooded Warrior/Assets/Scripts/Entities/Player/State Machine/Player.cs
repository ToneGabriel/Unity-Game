using UnityEngine;

public class Player : Entity
{
    #region Components & Data
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public GameObject DashDirectionIndicator;
    public GameObject LightOrbPosition;
    private Data_Player _dataPlayer;
    #endregion

    #region States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerDefendState SecondaryDefendState { get; private set; }
    public PlayerSpellState SpellCastState { get; private set; }
    #endregion

    #region Others
    private int _weaponIndex;
    private int _spellIndex;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        _dataPlayer = (Data_Player)_dataEntity;
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();

        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StateMachine.Initialize(IdleState);
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

        StateMachine.CurrentState.LogicUpdate();
    }

    protected override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Setters
    protected override void InitializeStates()
    {
        base.InitializeStates();

        IdleState = new PlayerIdleState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Idle_b);
        MoveState = new PlayerMoveState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Move_b);
        JumpState = new PlayerJumpState(this, StateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        InAirState = new PlayerInAirState(this, StateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        LandState = new PlayerLandState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Land_b);
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _dataPlayer, PlayerControllerParameters.WallSlide_b);
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _dataPlayer, PlayerControllerParameters.WallGrab_b);
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _dataPlayer, PlayerControllerParameters.WallClimb_b);
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, _dataPlayer, PlayerControllerParameters.LedgeClimbState_b);
        DashState = new PlayerDashState(this, StateMachine, _dataPlayer, PlayerControllerParameters.InAir_b);
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, _dataPlayer, PlayerControllerParameters.CrouchIdle_b);
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, _dataPlayer, PlayerControllerParameters.CrouchMove_b);
        RollState = new PlayerRollState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Roll_b);

        PrimaryAttackState = new PlayerAttackState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);
        SecondaryDefendState = new PlayerDefendState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);
        SpellCastState = new PlayerSpellState(this, StateMachine, _dataPlayer, PlayerControllerParameters.Combat_b);

        _weaponIndex = 0;
        _spellIndex = 0;
        PrimaryAttackState.SetWeapon(Inventory.Weapons[_weaponIndex]);
        SecondaryDefendState.SetShield(Inventory.Shield);
        SpellCastState.SetSpell(Inventory.Spells[_spellIndex]);
    }

    public void SetColiderHeight(float height)
    {
        Vector2 center = BoxCollider.offset;
        _workspaceVector2.Set(BoxCollider.size.x, height);

        center.y += (height - BoxCollider.size.y) / 2;

        BoxCollider.size = _workspaceVector2;
        BoxCollider.offset = center;
    }

    public void SetLightOrbPosition(Vector2 position)
    {
        LightOrbPosition.transform.localPosition = position;
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
        return !SecondaryDefendState.Shield.IsOnCooldown;
    }

    public bool CheckIfCanCastSpell()
    {
        return !SpellCastState.Spell.IsOnCooldown;
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
        return (!SecondaryDefendState.IsHolding || LastDamageDirection != FacingDirection);
    }

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        InterruptActions();
        DamageHop(_dataPlayer.DamageHopDirection, _dataPlayer.DamageHopSpeed);

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
        CurrentHealth = _dataPlayer.MaxHealth;
        transform.position = GameManager.Instance.GameStartPlayerPosition.position;
        transform.rotation = GameManager.Instance.GameStartPlayerPosition.rotation;
    }

    public void ChangeWeapon()
    {
        if (_weaponIndex < Inventory.Weapons.Length - 1)
            _weaponIndex++;
        else
            _weaponIndex = 0;
        PrimaryAttackState.SetWeapon(Inventory.Weapons[_weaponIndex]);
    }

    public void ChangeSpell()
    {
        if (_spellIndex < Inventory.Spells.Length - 1)
            _spellIndex++;
        else
            _spellIndex = 0;
        SpellCastState.SetSpell(Inventory.Spells[_spellIndex]);
    }

    private void InterruptActions()
    {
        // cancel spell
        if (StateMachine.CurrentState == SpellCastState)
            Inventory.Spells[_spellIndex].AnimationFinishTrigger();
        // cancel attack
        else if (StateMachine.CurrentState == PrimaryAttackState)
            Inventory.Weapons[_weaponIndex].AnimationFinishTrigger();
        // cancel defend
        else if (StateMachine.CurrentState == SecondaryDefendState)
            Inventory.Shield.AnimationFinishTrigger(); 
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_environmentCheck.transform.position, Vector2.right * FacingDirection, _dataPlayer.EnvironmentCheckDistance, _dataPlayer.WhatIsGround);
        float xDistance = xHit.distance;
        _workspaceVector2.Set(xDistance * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.transform.position + (Vector3)_workspaceVector2, Vector2.down, _ledgeCheck.transform.position.y - _environmentCheck.transform.position.y, _dataPlayer.WhatIsGround);
        float yDistance = yHit.distance;
        _workspaceVector2.Set(_environmentCheck.transform.position.x + xDistance * FacingDirection, _ledgeCheck.transform.position.y - yDistance);
        
        return _workspaceVector2;
    }

    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
        //Gizmos.DrawLine(_ledgeCheck.transform.position, _ledgeCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataPlayer.EnvironmentCheckDistance));
    }
    #endregion
}

