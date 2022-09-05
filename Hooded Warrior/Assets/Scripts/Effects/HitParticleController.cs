using UnityEngine;

public class HitParticleController : MonoBehaviour, IPoolComponent
{
    private void FinishAnim()
    {
        ObjectPoolManager.Instance.AddToPool(gameObject);
    }

}
