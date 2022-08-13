using System;
using UnityEngine;

public class HitParticleController : MonoBehaviour, IPoolComponent
{
    private void FinishAnim()
    {
        ObjectPoolManager.Instance.AddToPool(GetType(), gameObject);
    }

    public Type GetObjectType()
    {
        return GetType();
    }

}
