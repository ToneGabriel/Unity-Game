using UnityEngine;

[CreateAssetMenu(fileName = "newLightOrbSpellData", menuName = "Data/Spell Data/Light Orb Spell")]
public class LightOrbSpellData : ScriptableObject
{
    public float SpellLifeTime = 8f;
    public float SmoothSpeed = 0.05f;
    public float FlutterAcceleration = 10f;
    public float HoverCircleRange = 1.5f;
    public float InnerLightMaxInnerRadius = 2f;
    public float OuterLightMaxInnerRadius = 10f;

    public WaitForSeconds LightPrepareTime = new WaitForSeconds(1f);
    public WaitForSeconds LightChangeTime = new WaitForSeconds(0.01f);
    public WaitForSeconds HoverTime = new WaitForSeconds(1.3f);
}
