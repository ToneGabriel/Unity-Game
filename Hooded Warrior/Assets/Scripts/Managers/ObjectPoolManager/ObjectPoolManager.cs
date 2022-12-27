using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    private Dictionary<Type, Queue<GameObject>> _poolDictionary;
    [SerializeField] private Pool[] _pools;

    #region Unity Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            _poolDictionary = new Dictionary<Type, Queue<GameObject>>();
        }
    }
    
    private void Start()
    {
        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject instance = Instantiate(pool.Prefab);
                instance.transform.SetParent(transform);
                instance.SetActive(false);
                objectPool.Enqueue(instance);
            }

            _poolDictionary.Add(pool.Prefab.GetComponent<IPoolComponent>().GetType(), objectPool);
        }
    }
    #endregion

    #region Access Pools
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        _poolDictionary[instance.GetComponent<IPoolComponent>().GetType()].Enqueue(instance);
    }

    public ObjectType GetFromPool<ObjectType>(Vector3 positionToSet, Quaternion rotationToSet)
    {
        GameObject instance = _poolDictionary[typeof(ObjectType)].Dequeue();

        instance.transform.position = positionToSet;
        instance.transform.rotation = rotationToSet;
        instance.SetActive(true);

        return instance.GetComponent<ObjectType>();
    }

    public void ClearScene()    // clear scene from pool objects when loading
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                AddToPool(child.gameObject);
    }
    #endregion
}
