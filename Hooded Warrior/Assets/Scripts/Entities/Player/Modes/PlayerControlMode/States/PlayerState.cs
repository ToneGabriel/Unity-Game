
public abstract class PlayerState : EntityModeState
{
    protected PlayerControlMode     _playerControlMode;
    protected PlayerControlModeData _playerControlModeData;

    private bool _changeWeaponInput;
    private bool _changeSpellInput;

    public PlayerState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _playerControlMode      = mode;
        _playerControlModeData  = data;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _changeWeaponInput  = InputManager.Instance.ChangeWeaponInput;
        _changeSpellInput   = InputManager.Instance.ChangeSpellInput;

        // independent weapon change action
        if (_changeWeaponInput)
        {
            InputManager.Instance.UseChangeWeaponInput();
            //_player.ChangeWeapon();
        }
        // independent spell change action
        if (_changeSpellInput)
        {
            InputManager.Instance.UseChangeSpellInput();
            //_player.ChangeSpell();
        }
    }
}
