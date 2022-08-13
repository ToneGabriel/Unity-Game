using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    private Dictionary<Type, Queue<GameObject>> _poolDictionary;
    [SerializeField] private Pool[] _pools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    private void Start()
    {
        _poolDictionary = new Dictionary<Type, Queue<GameObject>>();

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

            _poolDictionary.Add(pool.Prefab.GetComponent<IPoolComponent>().GetObjectType(), objectPool);

        }
    }

    public void AddToPool(Type poolType, GameObject instance)
    {
        instance.SetActive(false);
        _poolDictionary[poolType].Enqueue(instance);
    }

    public GameObject GetFromPool(Type poolType, Vector3 positionToSet, Quaternion rotationToSet)
    {
        GameObject instance = _poolDictionary[poolType].Dequeue();

        instance.transform.position = positionToSet;
        instance.transform.rotation = rotationToSet;
        instance.SetActive(true);

        return instance;
    }

    public void ClearScene()    // clear scene from pool objects when loading
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                AddToPool(child.GetComponent<IPoolComponent>().GetObjectType(), child.gameObject);
    }
}
