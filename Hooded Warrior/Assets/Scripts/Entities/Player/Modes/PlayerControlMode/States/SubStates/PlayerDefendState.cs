
public sealed class PlayerDefendState : PlayerAbilityState
{
    public Shield Shield { get; private set; }
    public bool IsHolding { get; private set; }

    public PlayerDefendState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        Shield.EnterShield();
    }

    public override void Exit()
    {
        base.Exit();

        // TODO
        //_player._inputHandler.StopHoldingSecondaryDefendInput();      // make sure isHolding is FALSE (when interrupted)
        Shield.ExitShield();
    }

    public override void Update()
    {
        base.Update();

        IsHolding = InputManager.Instance.SecondaryDefendInput;

        if (!IsHolding && !Shield.IsOnCooldown)      // exit if stop holding
            Shield.LowerShield();
    }

    public void SetShield(Shield shield)
    {
        if (Shield != null)
            Shield.gameObject.SetActive(false);

        Shield = shield;
        shield.InitializeShield(this);
        Shield.gameObject.SetActive(true);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        //_player.SetVelocityZero();
    }

}