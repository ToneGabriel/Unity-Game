using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[PoolObject]
public class LightOrb : MonoBehaviour
{
    #region Components & Data
    [SerializeField] private Light2D            _innerLightComponent;
    [SerializeField] private Light2D            _outerLightComponent;
    [SerializeField] private LightOrbSpellData  _lightOrbSpellData;

    private Rigidbody2D                         _rigidbody;
    private GameObject                          _target;

    private FiniteStateMachine                  _stateMachine;
    private State[]                             _states;

    private float                               _spellCastTime;
    #endregion Components & Data

    #region Unity Functions
    private void Awake()
    {
        _rigidbody  = GetComponent<Rigidbody2D>();
        _target     = null;

        InitializeStates();
    }

    private void OnEnable()
    {
        _stateMachine.InitializeState(_states[(int)LightOrbStateID.Born]);

        _spellCastTime = Time.time;
    }

    private void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
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
        return false;
    }
    #endregion Checkers

    #region Setters
    public void ChangeState(int stateID)
    {
        _stateMachine.ChangeState(_states[stateID]);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        //_target = GameManager.Instance.Player.GetLightOrbPosition();
    }

    public void IncreaseLightRadius(float innerRadius, float outerRadius)
    {
        _outerLightComponent.pointLightInnerRadius += innerRadius;
        _outerLightComponent.pointLightOuterRadius += outerRadius;
    }

    public void DecreaseLightRadius(float innerRadius, float outerRadius)
    {
        _outerLightComponent.pointLightInnerRadius -= innerRadius;
        _outerLightComponent.pointLightOuterRadius -= outerRadius;
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
    private void InitializeStates()
    {
        _stateMachine = new FiniteStateMachine();
        _states = new State[(int)LightOrbStateID._Count];

        _states[(int)LightOrbStateID.Born] = new LightOrbBornState(this);
        _states[(int)LightOrbStateID.Grow] = new LightOrbGrowState(this);
        _states[(int)LightOrbStateID.Live] = new LightOrbLiveState(this);
        _states[(int)LightOrbStateID.Die]  = new LightOrbDieState(this);
    }

    public void Die()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }

    public void CheckOrbTime()
    {
        if ((Time.time >= _spellCastTime + _lightOrbSpellData.SpellLifeTime) || GameManager.Instance.Player.GeneralStatus.IsDead)
            StartCoroutine(DecreaseOrbLightRadius());

        if (GameManager.Instance.IsLoadingData)
            Destroy(gameObject);
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
