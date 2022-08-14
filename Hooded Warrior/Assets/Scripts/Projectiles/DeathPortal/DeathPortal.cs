using System;
using UnityEngine;

public class DeathPortal : MonoBehaviour, IPoolComponent
{
    private BoxCollider2D _boxCollider;
    private GameObject _target;
    private AttackDetails _attackDetails;
    private float _distanceToTargetX;
    [SerializeField] private DeathPortalData _deathPortalData;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _target = GameManager.Instance.Player.gameObject;
    }

    private void OnEnable()
    {
        SetPortalLocation();
        _attackDetails.DamageAmount = _deathPortalData.SpellDamage;
        _attackDetails.Position = transform.position;
    }

    private void SetPortalLocation()    // translate portal on X axis towards target
    {
        _distanceToTargetX = Mathf.Abs(_target.transform.position.x - transform.position.x);
        transform.Translate(_distanceToTargetX, 0f, 0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.Player.gameObject)
            other.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }

    public void TriggerColliderEnter()
    {
        _boxCollider.enabled = true;
    }

    public void FinishColliderEnter()
    {
        _boxCollider.enabled = false;
    }

    public void FinishPortalAttack()
    {
        ObjectPoolManager.Instance.AddToPool(gameObject);
    }

    public Type GetObjectType()
    {
        return GetType();
    }
}
