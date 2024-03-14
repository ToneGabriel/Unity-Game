using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public sealed partial class ObjectPoolManager
{
    private partial class ObjectPool
    {
        private Queue<GameObject>   _inactiveObjects;   // actual pool
        private HashSet<GameObject> _activeObjects;     // monitor for faster cleanup and maintenance

        private static int          _defaultCapacity    = 8;
        private static int          _minCount           = 2;

        public ObjectPool(GameObject original)
        {
            _inactiveObjects    = new Queue<GameObject>();
            _activeObjects      = new HashSet<GameObject>();

            Supply(original);
        }

        public GameObject Get(Vector3 positionToSet, Quaternion rotationToSet)
        {
            if (_inactiveObjects.Count <= _minCount)
                Resuply();

            GameObject instance = _inactiveObjects.Dequeue();
            instance.transform.SetPositionAndRotation(positionToSet, rotationToSet);
            instance.SetActive(true);
            _activeObjects.Add(instance);

            return instance;
        }

        public void Return(GameObject instance)
        {
            _activeObjects.Remove(instance);
            instance.SetActive(false);
            _inactiveObjects.Enqueue(instance);
        }

        public void ReturnAll()
        {
            while (_activeObjects.Any())
            {
                GameObject instance = _activeObjects.First();
                Return(instance);
            }
        }

        public void CheckActivity()     // shrink pool if no activity
        {
            if (false == _activeObjects.Any())
                Shrink();
        }

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
    }
}
