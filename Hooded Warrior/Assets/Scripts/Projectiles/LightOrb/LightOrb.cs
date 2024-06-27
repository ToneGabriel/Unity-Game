using UnityEngine;
using UnityEngine.Rendering.Universal;


[PoolObject]
public sealed class LightOrb : ModularBehaviour
{
    #region Components & Data
    [SerializeField] private Light2D            _innerLightComponent;
    [SerializeField] private Light2D            _outerLightComponent;
    [SerializeField] private LightOrbSpellData  _lightOrbSpellData;

    private Rigidbody2D                         _rigidbody;
    private GameObject                          _target;
    #endregion Components & Data

    #region Component Getters & Setters
    public LightOrbSpellData Data
    {
        get { return _lightOrbSpellData; }
    }

    public float InnerLightInnerRadius
    {
        get { return _innerLightComponent.pointLightInnerRadius; }
        set { _innerLightComponent.pointLightInnerRadius = value; }
    }

    public float InnerLightOuterRadius
    {
        get { return _innerLightComponent.pointLightOuterRadius; }
        set { _innerLightComponent.pointLightOuterRadius = value; }
    }

    public float OuterLightInnerRadius
    {
        get { return _outerLightComponent.pointLightInnerRadius; }
        set { _outerLightComponent.pointLightInnerRadius = value; }
    }

    public float OuterLightOuterRadius
    {
        get { return _outerLightComponent.pointLightOuterRadius; }
        set { _outerLightComponent.pointLightOuterRadius = value; }
    }
    #endregion Component Getters & Setters

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

    #region Setters
    public void SetTarget(GameObject target)
    {
        _target = target;
        //_target = GameManager.Instance.Player.GetLightOrbPosition();
    }

    public void MoveTowardsTarget(float speed)
    {
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, speed);
    }

    public void ApplyImpulse(Vector2 impulse)
    {
        _rigidbody.AddForce(impulse, ForceMode2D.Impulse);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void Die()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }
    #endregion Setters

    #region Other
    //protected override void FSMInitializeModes()
    //{
    //    //AddNewState((int)LightOrbStateID.Born, new LightOrbBornState(this));
    //    //AddNewState((int)LightOrbStateID.Live, new LightOrbLiveState(this));
    //    //AddNewState((int)LightOrbStateID.Die,  new LightOrbDieState(this));
    //}

    protected override bool FSMUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FSMFixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }
    #endregion Other
}
