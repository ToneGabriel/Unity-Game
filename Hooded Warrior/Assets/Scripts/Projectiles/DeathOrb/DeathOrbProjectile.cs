using System;
using UnityEngine;

public class DeathOrbProjectile : MonoBehaviour, IPoolComponent
{
    private Rigidbody2D _rigidbody;
    private GameObject _target;
    private AttackDetails _attackDetails;
    private Vector2 _direction;
    private float _rotateAmount;
    private float _spellCastTime;
    [SerializeField] private DeathOrbProjectileData _deathOrbProjectileData;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _attackDetails.DamageAmount = _deathOrbProjectileData.SpellDamage;
        _spellCastTime = Time.time;
    }

    private void FixedUpdate()
    {
        CheckOrbHit();
        SetHomigDirection();
    }

    private void SetHomigDirection()
    {
        _direction = (Vector2)_target.transform.position - _rigidbody.position;
        _direction.Normalize();
        _rotateAmount = Vector3.Cross(_direction, transform.right).z;

        _rigidbody.angularVelocity = -_rotateAmount * _deathOrbProjectileData.SpellRotateSpeed;
        _rigidbody.velocity = transform.right * _deathOrbProjectileData.SpellSpeed;
    }

    private void CheckOrbHit()
    {
        if (Time.time >= _spellCastTime + _deathOrbProjectileData.SpellLifeTime)
            ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
        else
        {
            Collider2D damageHit = Physics2D.OverlapCircle(transform.position, _deathOrbProjectileData.DamageRadius, _deathOrbProjectileData.WhatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(transform.position, _deathOrbProjectileData.DamageRadius, _deathOrbProjectileData.WhatIsGround);

            if (damageHit)
            {
                _attackDetails.Position = transform.position;
                damageHit.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
                ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
            }
            else if (groundHit)
                ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
        }
    }
    
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public Type GetObjectType()
    {
        return GetType();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _deathOrbProjectileData.DamageRadius);
    }
}
