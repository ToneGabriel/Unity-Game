using UnityEngine;

public class SpellAnimationTrigger : MonoBehaviour
{
    private Spell _spell;

    private void Start()
    {
        _spell = GetComponentInParent<Spell>();
    }

    private void AnimationTrigger()
    {
        _spell.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        _spell.AnimationFinishTrigger();
    }

    private void AnimationStartHoldingTrigger()
    {
        _spell.AnimationStartHoldingTrigger();
    }

    private void TriggerSpellAttack()
    {
        _spell.TriggerSpellAttack();
    }
}
