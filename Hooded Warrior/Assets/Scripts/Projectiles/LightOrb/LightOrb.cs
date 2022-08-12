using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOrb : MonoBehaviour
{
    public static LightOrb Instance { get; private set; }

    [SerializeField] private Light2D _innerLightComponent;
    [SerializeField] private Light2D _outerLightComponent;
    [SerializeField] private LightOrbSpellData _lightOrbSpellData;
    private Rigidbody2D _rigidbody;
    private GameObject _target;

    private float _spellCastTime;
    private bool _isCoroutineStarted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            _rigidbody = GetComponent<Rigidbody2D>();
            _target = GameManager.Instance.Player.LightOrbPosition;
        }
    }

    private void OnEnable()
    {
        _isCoroutineStarted = false;
        StartCoroutine(HoverDirection());
        StartCoroutine(IncreaseOrbLightRadius());
        _spellCastTime = Time.time;
    }

    private void FixedUpdate()
    {
        CheckOrbTime();
        SetOrbPosition();
    }

    #region Orb Functionality
    private void CheckOrbTime()
    {
        if ((Time.time >= _spellCastTime + _lightOrbSpellData.SpellLifeTime && !_isCoroutineStarted) || GameManager.Instance.Player.IsDead)
            StartCoroutine(DecreaseOrbLightRadius());

        if (GameManager.Instance.IsLoadingData)
            Destroy(gameObject);
    }

    private void SetOrbPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, _lightOrbSpellData.SmoothSpeed);

        // Random direction generator. This results in a "flutter" effect
        _rigidbody.AddForce(Random.insideUnitCircle * _lightOrbSpellData.FlutterAcceleration * Time.deltaTime, ForceMode2D.Impulse);
    }

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
        _isCoroutineStarted = true;

        while (_outerLightComponent.pointLightInnerRadius > 0f)
        {
            _outerLightComponent.pointLightInnerRadius -= 0.04f;
            _outerLightComponent.pointLightOuterRadius -= 0.11f;

            yield return null;
        }
        
        Destroy(gameObject);
    }
    #endregion
}
