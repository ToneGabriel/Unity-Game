using UnityEngine;

[CreateAssetMenu(fileName = "newLightOrbSpellData", menuName = "Data/Spell Data/Light Orb Spell")]
public class LightOrbSpellData : ScriptableObject
{
    // Editor ==================================================

    [SerializeField] private float _spellLifeTime                       = 8f;
    [SerializeField] private float _smoothSpeed                         = 0.05f;
    [SerializeField] private float _flutterAcceleration                 = 10f;
    [SerializeField] private float _hoverCircleRange                    = 1.5f;
    [SerializeField] private float _hoverTime                           = 1.3f;

    [SerializeField] private float _innerLightMaxInnerRadius            = 0.4f;
    [SerializeField] private float _innerLightInnerRadiusChangeRatio    = 0.01f;
    [SerializeField] private float _innerLightOuterRadiusChangeRatio    = 0.015f;

    [SerializeField] private float _outerLightMaxInnerRadius            = 10f;
    [SerializeField] private float _outerLightInnerRadiusChangeRatio    = 0.04f;
    [SerializeField] private float _outerLightOuterRadiusChangeRatio    = 0.11f;

    // Getters ==================================================

    public float SpellLifeTime                      { get { return _spellLifeTime; } }
    public float SmoothSpeed                        { get { return _smoothSpeed; } }
    public float FlutterAcceleration                { get { return _flutterAcceleration; } }
    public float HoverCircleRange                   { get { return _hoverCircleRange; } }
    public float HoverTime                          { get { return _hoverTime; } }

    public float InnerLightMaxInnerRadius           { get { return _innerLightMaxInnerRadius; } }
    public float InnerLightInnerRadiusChangeRatio   { get { return _innerLightInnerRadiusChangeRatio; } }
    public float InnerLightOuterRadiusChangeRatio   { get { return _innerLightOuterRadiusChangeRatio; } }

    public float OuterLightMaxInnerRadius           { get { return _outerLightMaxInnerRadius; } }
    public float OuterLightInnerRadiusChangeRatio   { get { return _outerLightInnerRadiusChangeRatio; } }
    public float OuterLightOuterRadiusChangeRatio   { get { return _outerLightOuterRadiusChangeRatio; } }
}
