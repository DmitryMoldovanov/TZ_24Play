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
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(this);
        }

        protected abstract void ResetObject();
    }
}