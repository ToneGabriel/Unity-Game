using UnityEngine;

public class ShieldAnimationTrigger : MonoBehaviour
{
    private Shield _shield;

    private void Start()
    {
        _shield = GetComponentInParent<Shield>();
    }

    private void AnimationTrigger()
    {
        _shield.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        _shield.AnimationFinishTrigger();
    }

    private void AnimationStartHoldingTrigger()
    {
        _shield.AnimationStartHoldingTrigger();
    }
}
