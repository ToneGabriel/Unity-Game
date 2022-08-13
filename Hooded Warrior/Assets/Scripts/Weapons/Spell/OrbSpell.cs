using UnityEngine;

public class OrbSpell : Spell
{
    private float _castAngleStep;
    private static int _activeOrbs;

    private void Start()
    {
        _activeOrbs = 0;
        _castAngleStep = Mathf.Abs(_spellData.MaxCastAngleZ - _spellData.MinCastAngleZ) / (_spellData.MaxNumberOfSpells - 1);
    }

    private void SetCastRotation()
    {
        _castPosition.transform.rotation = transform.rotation;   // reset castPosition rotation (to parent)

        if (_activeOrbs == _spellData.MaxNumberOfSpells)
            _activeOrbs = 0;
        _castPosition.transform.Rotate(0f, 0f, _spellData.MinCastAngleZ + _castAngleStep * _activeOrbs);
        _activeOrbs++;
    }

    public override void TriggerSpellAttack()
    {
        base.TriggerSpellAttack();

        for (int i = 0; i <= Mathf.FloorToInt(_cooldownStartTime - _spellHoldStartTime) && i <= _spellData.MaxNumberOfSpells - 1; i++)
        {
            SetCastRotation();
            ObjectPoolManager.Instance.GetFromPool(typeof(MagicOrb), _castPosition.transform.position, _castPosition.transform.rotation);
        }
    }
}
