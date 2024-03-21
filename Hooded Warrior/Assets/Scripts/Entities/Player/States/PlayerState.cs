
public abstract class PlayerState : EntityState
{
    protected Player _player;
    protected Data_Player _dataPlayer;

    private bool _changeWeaponInput;
    private bool _changeSpellInput;

    public PlayerState(Player player, FiniteStateMachine stateMachine, Data_Player dataPlayer, string animBoolName)
        : base(stateMachine, animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _dataPlayer = dataPlayer;
        _animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();

        _player.ObjectComponents.Animator.SetBool(_animBoolName, true);
        _isAnimationFinished = false;
    }

    public override void Exit()
    {
        base.Exit();

        _player.ObjectComponents.Animator.SetBool(_animBoolName, false);
    }

    public override void LogicUpdate()
    {
        _changeWeaponInput = _player._inputHandler.ChangeWeaponInput;
        _changeSpellInput = _player._inputHandler.ChangeSpellInput;

        // independent weapon change action
        if (_changeWeaponInput)
        {
            _player._inputHandler.UseChangeWeaponInput();
            _player.ChangeWeapon();
        }
        // independent spell change action
        if (_changeSpellInput)
        {
            _player._inputHandler.UseChangeSpellInput();
            _player.ChangeSpell();
        }
    }
}
