using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttackState _attackState;
    private int _attackCounter;
    private float _attackExitTime;
    private AttackDetails _attackDetails;

    [SerializeField] private Animator _baseAnim;
    [SerializeField] private Animator _weaponAnim;
    [SerializeField] private GameObject _attackPosition;
    [SerializeField] private WeaponData _weaponData;

    #region Weapon Logic
    public void InitializeWeapon(PlayerAttackState attackState)
    {
        _attackState = attackState;
        _attackDetails.DamageAmount = _weaponData.Damage;
        _attackDetails.StunDamageAmmount = _weaponData.StunDamage;
    }

    public void EnterWeapon()
    {
        CheckIfShouldResetAttack();

        _baseAnim.SetBool("attack", true);
        _weaponAnim.SetBool("attack", true);

        _baseAnim.SetInteger("attackCounter", _attackCounter);
        _weaponAnim.SetInteger("attackCounter", _attackCounter);

    }

    public void ExitWeapon()
    {
        _baseAnim.SetBool("attack", false);
        _weaponAnim.SetBool("attack", false);

        _attackCounter++;
        _attackExitTime = Time.time;

    }

    private void CheckIfShouldResetAttack()
    {
        if (Time.time >= _attackExitTime + _weaponData.AttackResetTime || _attackCounter >= _weaponData.MovementSpeed.Length)
            _attackCounter = 0;
    }

    #endregion

    #region Triggers

    public virtual void TriggerMeleeAttack()
    {
        _attackDetails.Position = transform.position;
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attackPosition.transform.position, _weaponData.AttackRadius, _weaponData.WhatIsEnemy);
        foreach (Collider2D collider in detectedObjects)
            collider.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }

    public virtual void AnimationFinishTrigger()
    {
        _attackState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        _attackState.SetPlayerVelocity(_weaponData.MovementSpeed[_attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        _attackState.SetPlayerVelocity(0f);
    }
    #endregion

    public void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(attackPosition.transform.position, weaponData.attackRadius);
    }

}
