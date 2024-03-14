using UnityEngine;

[PoolObject]
public class HitParticleController : MonoBehaviour
{
    private void FinishAnim()
    {
        ObjectPoolManager.Instance.ReturnToPool(this);
    }

}
