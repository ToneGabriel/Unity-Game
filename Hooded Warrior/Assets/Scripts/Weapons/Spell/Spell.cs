using UnityEngine;

public class Spell : MonoBehaviour, ICooldown
{
    protected PlayerSpellState _spellState;
    protected float _spellHoldStartTime;
    protected float _cooldownStartTime;

    public bool IsOnCooldown { get; private set; }

    [SerializeField] protected Animator _baseAnim;
    [SerializeField] protected Animator _spellAnim;
    [SerializeField] protected GameObject _castPosition;
    [SerializeField] protected SpellData _spellData;

    #region Spell Logic
    public void InitializeSpell(PlayerSpellState spellState)
    {
        _spellState = spellState;
    }

    public void EnterSpell()
    {
        _baseAnim.SetBool("cast", true);
        _spellAnim.SetBool("cast", true);
    }

    public void ExitSpell()
    {
        _baseAnim.SetBool("cast", false);
        _spellAnim.SetBool("cast", false);

        CooldownManager.Instance.Subscribe(this);
    }

    public void CastSpell()
    {
        if(_baseAnim.GetBool("isHolding"))
        {
            _baseAnim.SetBool("isHolding", false);
            _spellAnim.SetBool("isHolding", false);
        }
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _cooldownStartTime + _spellData.SpellCooldownTime)
        {
            IsOnCooldown = false;
            CooldownManager.Instance.UnSubscribe(this);
        }
    }
    #endregion

    #region Animation Triggers
    public virtual void TriggerSpellAttack()
    {
        IsOnCooldown = true;
        _cooldownStartTime = Time.time;
    }

    public virtual void AnimationTrigger()
    {
        _spellState.AnimationTrigger();
    }

    public virtual void AnimationFinishTrigger()
    {
        _spellState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartHoldingTrigger()
    {
        _baseAnim.SetBool("isHolding", true);
        _spellAnim.SetBool("isHolding", true);
        _spellHoldStartTime = Time.time;
    }
    #endregion
}
