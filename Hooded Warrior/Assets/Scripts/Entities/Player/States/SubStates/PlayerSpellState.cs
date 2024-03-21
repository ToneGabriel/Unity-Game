
public sealed class PlayerSpellState : PlayerAbilityState
{
    public Spell Spell { get; private set; }
    public bool IsHolding { get; private set; }

    public PlayerSpellState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        Spell.EnterSpell();
    }

    public override void Exit()
    {
        base.Exit();

        Spell.ExitSpell();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        IsHolding = _player._inputHandler.SpellCastInput;

        if (!IsHolding && !Spell.IsOnCooldown)
            Spell.CastSpell();
    }

    public void SetSpell(Spell spell)
    {
        if (Spell != null)
            Spell.gameObject.SetActive(false);

        Spell = spell;
        spell.InitializeSpell(this);
        Spell.gameObject.SetActive(true);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _player.SetVelocityZero();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }
}