
public abstract class PlayerState : EntityState
{
    protected Player _player;

    private bool _changeWeaponInput;
    private bool _changeSpellInput;

    public PlayerState(Player player, string animBoolName)
        : base(player, animBoolName)
    {
        _player = player;
    }

    public override void LogicUpdate()
    {
        _changeWeaponInput  = InputManager.Instance.ChangeWeaponInput;
        _changeSpellInput   = InputManager.Instance.ChangeSpellInput;

        // independent weapon change action
        if (_changeWeaponInput)
        {
            InputManager.Instance.ForceInputStop(ref InputManager.Instance.ChangeWeaponInput);
            _player.ChangeWeapon();
        }
        // independent spell change action
        if (_changeSpellInput)
        {
            InputManager.Instance.ForceInputStop(ref InputManager.Instance.ChangeSpellInput);
            _player.ChangeSpell();
        }
    }
}
