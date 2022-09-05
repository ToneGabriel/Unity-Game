﻿using UnityEngine;

public class Shield : MonoBehaviour, IDamageble
{
    private PlayerDefendState _defendState;
    private BoxCollider2D _shieldCollider;
    private float _shieldCurrentHealth;
    private float _shieldRegenerationTime;
    private float _regenerationStartTime;
    private float _shieldCooldownTime;

    public bool IsBroken { get; private set; }
    public bool IsOnCooldown { get; private set; }

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
        _shieldCurrentHealth = _shieldData.ShieldHealth;
        _shieldRegenerationTime = _shieldData.ShieldRegenerationTime;
        _shieldCooldownTime = _shieldData.ShieldCooldownTime;
    }

    public void EnterShield()
    {
        _shieldCollider.enabled = true;
        _shieldCurrentHealth = _shieldData.ShieldHealth;
        _baseAnimator.SetBool("defend", true);
        _shieldAnimator.SetBool("defend", true);
    }

    public void ExitShield()
    {
        _baseAnimator.SetBool("defend", false);
        _shieldAnimator.SetBool("defend", false);
    }
    
    public void LowerShield()
    {
        // if not already stop holding
        if(_baseAnimator.GetBool("isHolding"))
        {
            _baseAnimator.SetBool("isHolding", false);
            _shieldAnimator.SetBool("isHolding", false);

            IsOnCooldown = true;
            _regenerationStartTime = Time.time;
        }
    }

    private void Update()
    {
        // cooldonw if is broken
        if (IsBroken && Time.time >= _regenerationStartTime + _shieldRegenerationTime)
        {
            IsBroken = false;

            _baseAnimator.SetBool("isBroken", IsBroken);
            _shieldAnimator.SetBool("isBroken", IsBroken);
        }
        // cooldown if stop using
        if (!IsBroken && IsOnCooldown && Time.time >= _regenerationStartTime + _shieldCooldownTime)
            IsOnCooldown = false;
    }

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
        if (_shieldCurrentHealth <= 0)
        {
            IsBroken = true;
            _baseAnimator.SetBool("isBroken", IsBroken);
            _shieldAnimator.SetBool("isBroken", IsBroken);
        }
    }
    #endregion

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        _shieldCollider.enabled = false;
        _defendState.AnimationFinishTrigger();
    }

    public virtual void AnimationTrigger()
    {
        _defendState.AnimationTrigger();
    }

    public virtual void AnimationStartHoldingTrigger()
    {
        _baseAnimator.SetBool("isHolding", true);
        _shieldAnimator.SetBool("isHolding", true);
    }

    #endregion

}