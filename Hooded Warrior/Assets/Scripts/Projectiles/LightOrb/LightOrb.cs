using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[PoolObject]
public sealed class LightOrb : FSMMonoBehaviour
{
    #region Components & Data
    [SerializeField] private Light2D            _innerLightComponent;
    [SerializeField] private Light2D            _outerLightComponent;
    [SerializeField] private LightOrbSpellData  _lightOrbSpellData;

    private Rigidbody2D                         _rigidbody;
    private GameObject                          _target;

    private float                               _spellCastTime;
    #endregion Components & Data

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        _rigidbody  = GetComponent<Rigidbody2D>();
        _target     = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _spellCastTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion Unity Functions

    #region Checkers
    public bool CanGrow()
    {
        return true;
    }

    public bool IsOrbLightAtMaxRadius()
    {
        return _outerLightComponent.pointLightInnerRadius >= _lightOrbSpellData.OuterLightMaxInnerRadius;
    }

    public bool IsOrbLightAtZeroRadius()
    {
        return _outerLightComponent.pointLightInnerRadius <= 0f;
    }

    public bool IsReadyToDie()
    {
        return (Time.time >= _spellCastTime + _lightOrbSpellData.SpellLifeTime);
    }
    #endregion Checkers

    #region Setters
    public void SetTarget(GameObject target)
    {
        _target = target;
        //_target = GameManager.Instance.Player.GetLightOrbPosition();
    }

    public void IncreaseLightRadius()
    {
        _outerLightComponent.pointLightInnerRadius += _lightOrbSpellData.OuterLightInnerRadiusChangeRatio;
        _outerLightComponent.pointLightOuterRadius += _lightOrbSpellData.OuterLightOuterRadiusChangeRatio;
    }

    public void DecreaseLightRadius()
    {
        _outerLightComponent.pointLightInnerRadius -= _lightOrbSpellData.OuterLightInnerRadiusChangeRatio;
        _outerLightComponent.pointLightOuterRadius -= _lightOrbSpellData.OuterLightOuterRadiusChangeRatio;
    }

    public void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, _lightOrbSpellData.SmoothSpeed);

        // Random direction generator. This results in a "flutter" effect
        _rigidbody.AddForce(_lightOrbSpellData.FlutterAcceleration * Time.deltaTime * Random.insideUnitCircle, ForceMode2D.Impulse);
    }

    public void UpdateHoverDirection()
    {
        _rigidbody.velocity = Random.insideUnitCircle * _lightOrbSpellData.HoverCircleRange;
    }
    #endregion Setters

    #region Other
    protected override void InitializeStates()
    {
        AddNewState((int)LightOrbStateID.Born, new LightOrbBornState(this));
        AddNewState((int)LightOrbStateID.Grow, new LightOrbGrowState(this));
        AddNewState((int)LightOrbStateID.Live, new LightOrbLiveState(this));
        AddNewState((int)LightOrbStateID.Die,  new LightOrbDieState(this));
    }

    public void Die()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
    #endregion Other

    //A regular and prominant directional change, resulting in short darting motions around the target position
    private IEnumerator HoverDirection()
    {
        yield return _lightOrbSpellData.LightPrepareTime;

        while (gameObject)
        {
            _rigidbody.velocity = Random.insideUnitCircle * _lightOrbSpellData.HoverCircleRange;
            yield return _lightOrbSpellData.HoverTime;
        }
    }

    //Gradualy increase orb light radius
    private IEnumerator IncreaseOrbLightRadius()
    {
        yield return _lightOrbSpellData.LightPrepareTime;

        while (_outerLightComponent.pointLightInnerRadius < _lightOrbSpellData.OuterLightMaxInnerRadius)
        {
            _outerLightComponent.pointLightInnerRadius += 0.04f;
            _outerLightComponent.pointLightOuterRadius += 0.11f;

            yield return null;
        }
    }

    //Gradualy decrease orb light radius and destroy gameobject afterwards
    private IEnumerator DecreaseOrbLightRadius()
    {
        while (_outerLightComponent.pointLightInnerRadius > 0f)
        {
            _outerLightComponent.pointLightInnerRadius -= 0.04f;
            _outerLightComponent.pointLightOuterRadius -= 0.11f;

            yield return null;
        }
        
        Destroy(gameObject);
    }
}
