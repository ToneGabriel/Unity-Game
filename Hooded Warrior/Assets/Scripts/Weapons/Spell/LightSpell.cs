using UnityEngine;

public class LightSpell : Spell
{
    [SerializeField] private GameObject _lightOrbPrefab;

    public override void TriggerSpellAttack()
    {
        base.TriggerSpellAttack();

        LightOrb instance = ObjectPoolManager.Instance.GetFromPool<LightOrb>(_castPosition.transform.position, _castPosition.transform.rotation);
        // TODO
        //instance.SetTarget(null);

        //Instantiate(_lightOrbPrefab, _castPosition.transform.position, _castPosition.transform.rotation);
    }
}
