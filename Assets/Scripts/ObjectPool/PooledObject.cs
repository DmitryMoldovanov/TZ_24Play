using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.ObjectPool
{
    public abstract class PooledObject<T> : MonoBehaviour where T : class
    {
        private ObjectPool<T> _pool;

        protected ObjectPool<T> Pool => _pool;

        public void SetPool(ObjectPool<T> objectPool) => _pool = objectPool;

        public void ReturnToPool(T obj)
        {
            if (_pool is null)
            {
                Destroy(gameObject);
            }
            else
                _pool.Release(obj);
        }

        public void Enable()
        {
            try
            {
                gameObject.SetActive(true);
            }
            catch (MissingReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Disable()
        {
            try
            {
                gameObject.SetActive(true);
            }
            catch (MissingReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected abstract void ResetObject();
    }
}
