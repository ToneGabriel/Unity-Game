
public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    private float _velocityToSet;
    private bool _setVelocity;

    public PlayerAttackState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _setVelocity = false;
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_setVelocity)
            _player.SetVelocityX(_velocityToSet * _player.FacingDirection);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (_weapon != null)
            _weapon.gameObject.SetActive(false);

        _weapon = weapon;
        weapon.InitializeWeapon(this);
        _weapon.gameObject.SetActive(true);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }

    public void SetPlayerVelocity(float velocity)
    {
        _player.SetVelocityX(velocity * _player.FacingDirection);
        _velocityToSet = velocity;
        _setVelocity = true;
    }

}
