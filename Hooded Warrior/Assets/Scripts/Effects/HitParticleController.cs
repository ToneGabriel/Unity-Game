using System;
using UnityEngine;

public class HitParticleController : MonoBehaviour, IPoolComponent
{
    private void FinishAnim()
    {
        ObjectPoolManager.Instance.AddToPool(gameObject);
    }

    public Type GetObjectType()
    {
        return GetType();
    }

}
