using UnityEngine;

[CreateAssetMenu(fileName = "newLightOrbSpellData", menuName = "Data/Spell Data/Light Orb Spell")]
public class LightOrbSpellData : ScriptableObject
{
    // Editor ==================================================
    [SerializeField] private float _spellLifeTime                      = 8f;
    [SerializeField] private float _smoothSpeed                        = 0.05f;
    [SerializeField] private float _flutterAcceleration                = 10f;
    [SerializeField] private float _hoverCircleRange                   = 1.5f;
    [SerializeField] private float _innerLightMaxInnerRadius           = 2f;
    [SerializeField] private float _outerLightMaxInnerRadius           = 10f;
    [SerializeField] private float _outerLightInnerRadiusChangeRatio   = 0.04f;
    [SerializeField] private float _outerLightOuterRadiusChangeRatio   = 0.11f;
    [SerializeField] private WaitForSeconds _lightPrepareTime          = new WaitForSeconds(1f);
    [SerializeField] private WaitForSeconds _lightChangeTime           = new WaitForSeconds(0.01f);
    [SerializeField] private WaitForSeconds _hoverTime                 = new WaitForSeconds(1.3f);

    // Getters ==================================================

    public float SpellLifeTime                      { get { return _spellLifeTime; } }
    public float SmoothSpeed                        { get { return _smoothSpeed; } }
    public float FlutterAcceleration                { get { return _flutterAcceleration; } }
    public float HoverCircleRange                   { get { return _hoverCircleRange; } }
    public float InnerLightMaxInnerRadius           { get { return _innerLightMaxInnerRadius; } }
    public float OuterLightMaxInnerRadius           { get { return _outerLightMaxInnerRadius; } }
    public float OuterLightInnerRadiusChangeRatio   { get { return _outerLightInnerRadiusChangeRatio; } }
    public float OuterLightOuterRadiusChangeRatio   { get { return _outerLightOuterRadiusChangeRatio; } }
    public WaitForSeconds LightPrepareTime          { get { return _lightPrepareTime; } }
    public WaitForSeconds LightChangeTime           { get { return _lightChangeTime; } }
    public WaitForSeconds HoverTime                 { get { return _hoverTime; } }
}
