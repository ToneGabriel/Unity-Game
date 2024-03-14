using UnityEngine;

[PoolObject]
public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private AttackDetails _attackDetails;
    [SerializeField] private GameObject _damagePosition;
    [SerializeField] private ArrowData _arrowData;

    private float _startPositionX;
    private float _groundedStartTime;
    private float _rotateAmount;
    private bool _isOnGround;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _attackDetails.DamageAmount = _arrowData.ArrowDamage;
        _startPositionX = transform.position.x;
        _isOnGround = false;
    }

    private void FixedUpdate()
    {
        SetArrowDirection();
        CheckArrowHit();
    }

    private void CheckArrowHit()                                                    // Check if arrow hit player or ground
    {
        if (!_isOnGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.transform.position, _arrowData.DamageRadius, _arrowData.WhatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.transform.position, _arrowData.DamageRadius, _arrowData.WhatIsGround);

            if (damageHit)      // if player hit - disable arrow
            {
                _attackDetails.Position = transform.position;
                damageHit.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
                ObjectPoolManager.Instance.ReturnToPool(this);
            }
            else if (groundHit)     // if ground hit - wait before disable
            {
                _isOnGround = true;
                _groundedStartTime = Time.time;
            }
        }
        else if (Time.time >= _groundedStartTime + _arrowData.ArrowGroundedTime)      // disable arrow after time on ground
            ObjectPoolManager.Instance.ReturnToPool(this);
    }

    private void SetArrowDirection()                                                // arrow movement
    {
        if(!_isOnGround)
        {
            if (Mathf.Abs(_startPositionX - transform.position.x) >= _arrowData.ArrowMaxTravelDistance)
                _rotateAmount = Vector3.Cross(-GameManager.Instance.transform.up, transform.right).z;     // if max distance is reached -> arrow point down
            else
                _rotateAmount = 0f;

            _rigidbody2D.angularVelocity = -_rotateAmount * _arrowData.ArrowRotateSpeed;
            _rigidbody2D.velocity = transform.right * _arrowData.ArrowSpeed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(damagePosition.transform.position, damageRadius);
    }
}
