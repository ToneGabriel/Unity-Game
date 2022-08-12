using UnityEngine;

public class LightSpell : Spell
{
    [SerializeField] private GameObject _lightOrbPrefab;

    public override void TriggerSpellAttack()
    {
        base.TriggerSpellAttack();

        Instantiate(_lightOrbPrefab, _castPosition.transform.position, _castPosition.transform.rotation);
    }
}
