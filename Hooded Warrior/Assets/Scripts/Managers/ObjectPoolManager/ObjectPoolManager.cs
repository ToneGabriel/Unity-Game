using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;
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
        SetPoolTags();

        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject instance = Instantiate(pool.Prefab);
                instance.transform.SetParent(transform);
                instance.SetActive(false);
                objectPool.Enqueue(instance);
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public void AddToPool(string poolTag, GameObject instance)
    {
        instance.SetActive(false);
        _poolDictionary[poolTag].Enqueue(instance);
    }

    public GameObject GetFromPool(string poolTag, Vector3 positionToSet, Quaternion rotationToSet)
    {
        GameObject instance = _poolDictionary[poolTag].Dequeue();

        instance.transform.position = positionToSet;
        instance.transform.rotation = rotationToSet;
        instance.SetActive(true);

        return instance;
    }

    public void ClearScene()    // clear scene from pool objects when loading
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                AddToPool(child.GetComponent<IPoolComponent>().GetTag(), child.gameObject);
    }

    private void SetPoolTags()                          // Set the tag of the pool prefab equal to pool tag
    {                                                   // All objects in pool have the same pool tag
        foreach (Pool pool in _pools)
            pool.Prefab.GetComponent<IPoolComponent>().SetTag(pool.Tag);

    }
}
