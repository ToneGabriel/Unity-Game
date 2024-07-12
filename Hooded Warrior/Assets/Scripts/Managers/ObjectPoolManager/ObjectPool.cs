using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public sealed partial class ObjectPoolManager
{
    private partial class ObjectPool
    {
        #region Components & Data
        private readonly Queue<GameObject>   _inactiveObjects;   // actual pool
        private readonly HashSet<GameObject> _activeObjects;     // monitor for faster cleanup and maintenance

        private static readonly int _defaultCapacity    = 8;
        private static readonly int _minCount           = 2;
        #endregion Components & Data

        #region Pool Interface
        public ObjectPool(GameObject original)
        {
            _inactiveObjects    = new Queue<GameObject>();
            _activeObjects      = new HashSet<GameObject>();

            Supply(original);
        }

        public GameObject GetObject(Vector3 positionToSet, Quaternion rotationToSet)
        {
            if (_inactiveObjects.Count <= _minCount)
                Resuply();

            GameObject instance = _inactiveObjects.Dequeue();
            instance.transform.SetPositionAndRotation(positionToSet, rotationToSet);
            instance.SetActive(true);
            _activeObjects.Add(instance);

            return instance;
        }

        public void ReturnObject(GameObject instance)
        {
            _activeObjects.Remove(instance);
            instance.SetActive(false);
            _inactiveObjects.Enqueue(instance);
        }

        public void ReturnAll()
        {
            while (_activeObjects.Any())
                ReturnObject(_activeObjects.First());
        }

        public void CheckActivity()     // shrink pool if no activity
        {
            if (false == _activeObjects.Any())
                Shrink();
        }
        #endregion Pool Interface

        #region Helpers
        private void Supply(GameObject original)
        {
            for (int i = 0; i < _defaultCapacity; ++i)
            {
                GameObject instance = GameObject.Instantiate(original);
                instance.transform.SetParent(Instance.transform);   // Instance of ObjectPoolManager
                instance.SetActive(false);
                _inactiveObjects.Enqueue(instance);
            }
        }

        private void Resuply()
        {
            // duplicate a remaining item
            Supply(_inactiveObjects.First());
        }

        private void Shrink()
        {
            int shrinkCount = _inactiveObjects.Count / 4;  // shrink by 25%
            for (int i = 0; i < shrinkCount && _inactiveObjects.Count > _minCount; ++i)
                GameObject.Destroy(_inactiveObjects.Dequeue());
        }
        #endregion Helpers
    }
}
