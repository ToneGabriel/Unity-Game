using UnityEngine;


[CreateAssetMenu(fileName = "newSpellData", menuName = "Data/Spell Data/Base Spell")]
public class SpellData : ScriptableObject
{
    public int MaxNumberOfSpells = 3;
    public float MinCastAngleZ = -30f;
    public float MaxCastAngleZ = 60f;
    public float SpellCooldownTime = 10f;
}
