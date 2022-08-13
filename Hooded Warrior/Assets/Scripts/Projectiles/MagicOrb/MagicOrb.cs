using System;
using UnityEngine;

public class MagicOrb : MonoBehaviour, IPoolComponent
{
    private Rigidbody2D _rigidbody;
    private GameObject _target;
    private AttackDetails _attackDetails;
    private Vector2 _direction;
    private float _rotateAmount;
    private float _spellCastTime;
    private float _minDistance;
    private float _distanceToTarget;
    [SerializeField] private OrbSpellData _orbSpellData;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _attackDetails.DamageAmount = _orbSpellData.SpellDamage;

        _minDistance = _orbSpellData.DetectionRadius;
        _spellCastTime = Time.time;
        _target = null;
    }

    private void FixedUpdate()
    {
        SelectTarget();
        CheckOrbHit();
        SetHomigDirection();
    }

    private void SelectTarget()
    {
        if(!_target)
        {
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, _orbSpellData.DetectionRadius, _orbSpellData.WhatIsEnemy);
            foreach (Collider2D collider in detectedEnemies)
            {
                _distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (_distanceToTarget <= _minDistance)   // select closest target
                {
                    _minDistance = _distanceToTarget;
                    _target = collider.gameObject;
                }
            }
        }
    }

    private void SetHomigDirection()
    {
        if (_target)
        {
            _direction = (Vector2)_target.transform.position - _rigidbody.position;
            _direction.Normalize();
            _rotateAmount = Vector3.Cross(_direction, transform.right).z;
        }
        else
            _rotateAmount = UnityEngine.Random.Range(-5f, 5f);   // random rotation if target not found

        _rigidbody.angularVelocity = -_rotateAmount * _orbSpellData.SpellRotateSpeed;
        _rigidbody.velocity = transform.right * _orbSpellData.SpellSpeed;
    }

    private void CheckOrbHit()
    {
        if (Time.time >= _spellCastTime + _orbSpellData.SpellLifeTime)
            ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
        else
        {
            Collider2D damageHit = Physics2D.OverlapCircle(transform.position, _orbSpellData.DamageRadius, _orbSpellData.WhatIsEnemy);
            Collider2D groundHit = Physics2D.OverlapCircle(transform.position, _orbSpellData.DamageRadius, _orbSpellData.WhatIsGround);

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

    public Type GetObjectType()
    {
        return GetType();
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, orbSpellData.detectionRadius);
    }

}
