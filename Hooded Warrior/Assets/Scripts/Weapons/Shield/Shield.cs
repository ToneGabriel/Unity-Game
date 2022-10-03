using UnityEngine;

public class Shield : MonoBehaviour, IDamageble, ICooldown
{
    public bool IsOnCooldown { get; private set; }

    private PlayerDefendState _defendState;
    private BoxCollider2D _shieldCollider;
    private float _shieldCurrentHealth;
    private float _cooldownStartTime;
    private float _currentCooldownTime;

    [SerializeField] private Animator _baseAnimator;
    [SerializeField] private Animator _shieldAnimator;
    [SerializeField] private ShieldData _shieldData;

    #region Weapon Logic
    private void Awake()
    {
        _shieldCollider = GetComponent<BoxCollider2D>();
    }

    public void InitializeShield(PlayerDefendState defendState)
    {
        _defendState = defendState;
    }

    public void EnterShield()
    {
        SetAnimatorDefend(true);

        _shieldCollider.enabled = true;
        _shieldCurrentHealth = _shieldData.ShieldHealth;
    }

    public void ExitShield()
    {
        SetAnimatorDefend(false);

        CooldownManager.Instance.Subscribe(this);
    }
    
    public void LowerShield()       // exit if not holding
    {
        SetAnimatorHolding(false);

        IsOnCooldown = true;
        _cooldownStartTime = Time.time;
        _currentCooldownTime = _shieldData.ShieldCooldownTime;
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _cooldownStartTime + _currentCooldownTime)
        {
            IsOnCooldown = false;
            CooldownManager.Instance.UnSubscribe(this);
        }
    }
    #endregion

    #region Damage
    public void Damage(AttackDetails attackdetails)
    {
        if(CanTakeDamage())
        {
            AdditionalDamageActions(attackdetails);

            _shieldCurrentHealth -= attackdetails.DamageAmount;

            CheckStatus();
        }
    }

    public bool CanTakeDamage() => true;

    public void AdditionalDamageActions(AttackDetails attackDetails) { }

    public void CheckStatus()
    {
        if (_shieldCurrentHealth <= 0)      // exit if shield is broken
        {
            SetAnimatorBroken(true);

            IsOnCooldown = true;
            _cooldownStartTime = Time.time;
            _currentCooldownTime = _shieldData.ShieldRegenerationTime;
        }
    }
    #endregion

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        SetAnimatorHolding(false);
        _shieldCollider.enabled = false;
        _defendState.AnimationFinishTrigger();
    }

    public virtual void AnimationTrigger()
    {
        SetAnimatorHolding(true);
        SetAnimatorBroken(false);
        _defendState.AnimationTrigger();
    }

    public virtual void AnimationStartHoldingTrigger() { }
    #endregion

    #region Animator Setters
    private void SetAnimatorDefend(bool state)
    {
        _baseAnimator.SetBool("defend", state);
        _shieldAnimator.SetBool("defend", state);
    }

    private void SetAnimatorHolding(bool state)
    {
        _baseAnimator.SetBool("isHolding", state);
        _shieldAnimator.SetBool("isHolding", state);
    }

    private void SetAnimatorBroken(bool state)
    {
        _baseAnimator.SetBool("isBroken", state);
        _shieldAnimator.SetBool("isBroken", state);
    }
    #endregion
}
