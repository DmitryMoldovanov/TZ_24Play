using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.ObjectPool
{
    public abstract class PooledObject<T> : MonoBehaviour where T : class
    {
        private ObjectPool<T> _pool;

        protected ObjectPool<T> Pool => _pool;

        public void SetPool(ObjectPool<T> objectPool) => _pool = objectPool;

        public void ReturnToPool(T obj) => _pool.Release(obj);

        public void Enable()
        {
            if (gameObject is not null)
                gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (gameObject is not null)
                gameObject.SetActive(false);
        }

        protected abstract void ResetObject();
    }
}
