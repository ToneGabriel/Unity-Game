using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Use this attribute on components that are pooled
[AttributeUsage(AttributeTargets.Class)]
public class PoolObjectAttribute : Attribute { }


// Manages groups of small objects and cycles them as the game asks and dispozes them
// RequestPool should be used when the object that uses GetFromPool is spawned
// ReturnToPool should be used by the pooled object
// Important - Pools are mapped with the name of the data type
public sealed partial class ObjectPoolManager : MonoBehaviour
{
    #region Helpers Declarations
    private partial class ObjectPool { }
    #endregion Helpers Declarations

    #region Components & Data
    public static ObjectPoolManager Instance;

    private Dictionary<string, GameObject>  _prefabDatabase;    // All poolable prefab references
    private Dictionary<string, ObjectPool>  _pools;             // Manages groups of objects by their type name

    private float                           _checkTime;
    private float                           _checkInterval;
    private int                             _poolIndex;
    #endregion Components & Data

    #region Unity Functionality
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance        = this;

            _prefabDatabase = new Dictionary<string, GameObject>();
            _pools          = new Dictionary<string, ObjectPool>();
        }
    }

    private void Start()
    {
        // Load database
        var poolObjects = Resources.LoadAll<GameObject>("PoolObject Prefabs");

        foreach (var prefab in poolObjects)
        {
            // search all MonoBehaviour components attached to the GameObject and...
            // ...find the one with attribute PoolObjectAttribute (should be only one)

            var mbComponents    = prefab.GetComponents<MonoBehaviour>();
            bool found          = false;

            foreach (var comp in mbComponents)
            {
                if (HasPoolObjectAttribute(comp))
                {
                    _prefabDatabase.Add(GetKey(comp), prefab);
                    found = true;
                }
            }

            if (false == found)
                Debug.Log(prefab.ToString() + " is not marked with [PoolObject] attribute; Remove prefab from folder.");
        }

        // Initialize checkers
        _checkTime      = Time.time;
        _checkInterval  = 3f;
        _poolIndex      = 0;
    }

    private void Update()
    {
        // check activity of 1 pool at specified time interval
        if (_pools.Any() && Time.time >= _checkTime + _checkInterval)
        {
            if (_poolIndex == _pools.Count)     // end of list, return to first
                _poolIndex = 0;

            _pools.ElementAt(_poolIndex++).Value.CheckActivity();
            _checkTime = Time.time;
        }
        else
        {
            // wait...
        }
    }
    #endregion Unity Functionality

    #region Manage Pools
    public void RequestPool<TyObject>()
    where TyObject : MonoBehaviour
    {
        var key = GetKey<TyObject>();

        if (false == HasPoolObjectAttribute<TyObject>())
            throw new ArgumentException("Requested " + key + " Type is not marked with [PoolObject] attribute.");
        else
        {
            if (false == _pools.ContainsKey(key))   // no pool exists - create and add pool to map
            {
                if (false == _prefabDatabase.TryGetValue(key, out var prefab))
                    throw new ArgumentException("Requested " + key + " Type is not available as a prefab. Did you forget to add it in the folder?");
                else
                    _pools.Add(key, new ObjectPool(prefab));
            }
            else
            {
                // do nothing - the pool exists for the requested type
            }
        }
    }

    public void DeletePool<TyObject>()
    where TyObject : MonoBehaviour      // Use this only if it is certain that the pool is no longer needed
    {
        // TODO: implement
    }

    public TyObject GetFromPool<TyObject>(Vector3 positionToSet, Quaternion rotationToSet)    // ask for object of the needed Type
    where TyObject : MonoBehaviour
    {
        var key = GetKey<TyObject>();

        if (false == _pools.TryGetValue(key, out var currentPool))
            throw new NullReferenceException("No such pool exists. Try call RequestPool<" + key + ">() first.");
        else
            return currentPool.Get(positionToSet, rotationToSet).GetComponent<TyObject>();
    }

    public TyObject GetFromPool<TyObject>()    // default position and rotation
    where TyObject : MonoBehaviour
    {
        return GetFromPool<TyObject>(Vector3.zero, Quaternion.identity);
    }

    public void ReturnToPool<TyObject>(TyObject instance)   // put object back to pool
    where TyObject : MonoBehaviour
    {
        // guaranteed to have pool because it cannot be spawned otherwise
        _pools[GetKey<TyObject>()].Return(instance.gameObject);
    }

    public void ClearScene()    // clear scene for pool objects (ex: when game is loading)
    {
        foreach (var pool in _pools.Values)
            pool.ReturnAll();
    }

    private string GetKey<TyObject>()
    where TyObject : MonoBehaviour
    {
        return typeof(TyObject).Name;
    }

    private string GetKey(MonoBehaviour val)
    {
        return val.GetType().Name;
    }

    private bool HasPoolObjectAttribute<TyObject>()
    where TyObject : MonoBehaviour
    {
        return null != Attribute.GetCustomAttribute(typeof(TyObject), typeof(PoolObjectAttribute));
    }

    private bool HasPoolObjectAttribute(MonoBehaviour val)
    {
        return null != Attribute.GetCustomAttribute(val.GetType(), typeof(PoolObjectAttribute));
    }
    #endregion Manage Pools
}