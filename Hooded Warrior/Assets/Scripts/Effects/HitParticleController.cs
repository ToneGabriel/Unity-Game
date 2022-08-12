using UnityEngine;

public class HitParticleController : MonoBehaviour, IPoolComponent
{
    public static string PoolTag;

    private void FinishAnim()
    {
        ObjectPoolManager.Instance.AddToPool(PoolTag, gameObject);
    }

    public string GetTag()
    {
        return PoolTag;
    }

    public void SetTag(string tag)
    {
        PoolTag = tag;
    }

}
