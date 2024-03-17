using UnityEngine;

public class Spell : MonoBehaviour, ICooldown
{
    public bool IsOnCooldown { get; private set; }

    protected State _spellState;
    protected float _spellHoldStartTime;
    protected float _cooldownStartTime;

    [SerializeField] protected Animator _baseAnimator;
    [SerializeField] protected Animator _spellAnimator;
    [SerializeField] protected GameObject _castPosition;
    [SerializeField] protected SpellData _spellData;

    #region Spell Logic
    public void InitializeSpell(State spellState)
    {
        _spellState = spellState;
    }

    public void EnterSpell()
    {
        SetAnimatorCast(true);
    }

    public void ExitSpell()
    {
        SetAnimatorCast(false);

        CooldownManager.Instance.Subscribe(this);
    }

    public void CastSpell()     // TODO: check on interrupt
    {
        SetAnimatorHolding(false);

        IsOnCooldown = true;
        _cooldownStartTime = Time.time;
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _cooldownStartTime + _spellData.SpellCooldownTime)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        CooldownManager.Instance.UnSubscribe(this);
    }
    #endregion

    #region Animation Triggers
    public virtual void TriggerSpellAttack() { }

    public virtual void AnimationTrigger()
    {
        SetAnimatorHolding(true);
        _spellState.AnimationTrigger();
    }

    public virtual void AnimationFinishTrigger()
    {
        _spellState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartHoldingTrigger()
    {
        _spellHoldStartTime = Time.time;
    }
    #endregion

    #region Animator Setters
    private void SetAnimatorCast(bool state)
    {
        _baseAnimator.SetBool("cast", state);
        _spellAnimator.SetBool("cast", state);
    }

    private void SetAnimatorHolding(bool state)
    {
        _baseAnimator.SetBool("isHolding", state);
        _spellAnimator.SetBool("isHolding", state);
    }
    #endregion
}
