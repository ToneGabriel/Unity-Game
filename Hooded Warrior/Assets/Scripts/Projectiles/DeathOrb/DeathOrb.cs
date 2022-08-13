using System;
using System.Collections;
using UnityEngine;

public class DeathOrb : MonoBehaviour, IPoolComponent
{
    private GameObject _target;
    private float _spellCastTime;
    private bool _isCoroutineStarted;
    private int _activeProjectiles;
    private float _castAngleStep;
    private Vector3 _scaleUnit;
    [SerializeField] private DeathOrbData _deathOrbData;

    private void Awake()
    {
        _castAngleStep = Mathf.Abs(_deathOrbData.MaxCastAngleZ - _deathOrbData.MinCastAngleZ) / (_deathOrbData.MaxNumberOfProjectiles - 1);
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        _scaleUnit.Set(_deathOrbData.ScaleUnit, _deathOrbData.ScaleUnit, 0f);
        _target = null;
        _isCoroutineStarted = false;
        _activeProjectiles = 0;
        StartCoroutine(GrowOrb());
        _spellCastTime = Time.time;
    }

    private void FixedUpdate()
    {
        CheckOrbTime();
        SelectTarget();
    }

    private void SelectTarget()
    {
        if (!_target && Time.time >= _spellCastTime + _deathOrbData.TimeBeforeAttack)
        {
            Collider2D detectedTarget = Physics2D.OverlapCircle(transform.position, _deathOrbData.DetectionRadius, _deathOrbData.WhatIsPlayer);
            if(detectedTarget)    
                _target = detectedTarget.gameObject;
        }
        else if (_target && !_isCoroutineStarted)
        {
            for (int i = 0; i < _deathOrbData.MaxNumberOfProjectiles; i++)
            {
                SetCastRotation();
                ObjectPoolManager.Instance.GetFromPool(typeof(DeathOrbProjectile), transform.position, transform.rotation).GetComponent<DeathOrbProjectile>().SetTarget(_target);
            }

            StartCoroutine(KillOrb());
        }
    }

    private void CheckOrbTime()
    {
        if (Time.time >= _spellCastTime + _deathOrbData.LifeTime && !_isCoroutineStarted)
            StartCoroutine(KillOrb());
    }

    private void SetCastRotation()
    {
        transform.rotation = Quaternion.identity;   // reset castPosition rotation

        transform.Rotate(0f, 0f, _deathOrbData.MinCastAngleZ + _castAngleStep * _activeProjectiles);
        _activeProjectiles++;
    }

    private IEnumerator GrowOrb()
    {
        while (transform.localScale.x < _deathOrbData.MaxScale)
        {
            transform.localScale += _scaleUnit;

            yield return _deathOrbData.TimeToScale;
        }
    }

    private IEnumerator KillOrb()
    {
        _isCoroutineStarted = true;

        yield return new WaitForSeconds(_deathOrbData.TimeBeforeAttack);

        while (transform.localScale.x > 0f)
        {
            transform.localScale -= _scaleUnit;

            yield return _deathOrbData.TimeToScale;
        }

        ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
    }

    public Type GetObjectType()
    {
        return GetType();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _deathOrbData.DetectionRadius);
    }

}
