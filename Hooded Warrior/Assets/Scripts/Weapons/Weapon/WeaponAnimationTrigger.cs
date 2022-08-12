using UnityEngine;

public class WeaponAnimationTrigger : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponentInParent<Weapon>();
    }

    private void TriggerMeleeAttack()
    {
        _weapon.TriggerMeleeAttack();
    }

    private void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        _weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        _weapon.AnimationStopMovementTrigger();
    }

}
