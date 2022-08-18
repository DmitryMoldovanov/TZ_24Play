using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.ObjectPool
{
    public class ObjPool<T> where T : PooledObject<T>
    {
        private readonly ObjectPool<T> _pool;
        private readonly T[] _prefabs;
        private readonly T _prefab;
        private readonly Transform _poolParent;
        private readonly bool _isArrayPrefab;

        public ObjectPool<T> Pool => _pool;

        public ObjPool(T[] prefab, Transform poolParent, int startingPoolSize, bool preCreateObjects)
        {
            _prefabs = prefab;
            _poolParent = poolParent;
            _isArrayPrefab = true;

            _pool = new ObjectPool<T>(
                CreateObjectFromArray,
                OnGetFromPool,
                OnReturnToPool,
                null, false,
                startingPoolSize);

            if (preCreateObjects) PreCreateObjects(startingPoolSize);
        }
        
        public ObjPool(T prefab, Transform poolParent, int startingPoolSize, bool preCreateObjects)
        {
            _prefab = prefab;
            _poolParent = poolParent;
            _isArrayPrefab = false;

            _pool = new ObjectPool<T>(
                CreateObject,
                OnGetFromPool,
                OnReturnToPool,
                null, false,
                startingPoolSize);

            if (preCreateObjects) PreCreateObjects(startingPoolSize);
        }

        private void PreCreateObjects(int startingPoolSize)
        {
            for (int i = 0; i < startingPoolSize; i++)
            {
                if (_isArrayPrefab)
                    CreateObjectFromArray();
                else
                    CreateObject();
            }
        }

        private T CreateObjectFromArray()
        {
            int randomPrefab = Random.Range(0, _prefabs.Length);
            var obj = Object.Instantiate(_prefabs[randomPrefab], _poolParent);
            obj.SetPool(_pool);
            obj.ReturnToPool(obj);
            return obj;
        }
        
        private T CreateObject()
        {
            var obj = Object.Instantiate(_prefab, _poolParent);
            obj.SetPool(_pool);
            obj.ReturnToPool(obj);
            return obj;
        }

        private void OnGetFromPool(T obj)
        {
            obj.Enable();
        }

        private void OnReturnToPool(T obj)
        {
            obj.Disable();
        }
    }
}
