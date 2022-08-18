using System.Threading.Tasks;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class AttachableCube : PooledObject<AttachableCube>, IAttachable
    {
        [SerializeField] private bool _attached;
        [SerializeField] private CubeHolder _cubeHolder;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _collisionLineLength;

        private readonly int _resetCubeDelayInSeconds = 3;

        private Transform _transform;
        private float _colliderYBounds;

        void Awake()
        {
            _transform = transform;
            _colliderYBounds = GetComponent<BoxCollider>().size.y;
        }

        public void Initialize(Transform parent, Vector3 position)
        {
            _transform.parent = parent;
            _transform.localPosition = position;
        }

        private void FixedUpdate()
        {
            if (_attached && Physics.Linecast(
                    _transform.position,
                    _transform.position + new Vector3(0, 0, _collisionLineLength),
                    _layerMask.value))
            {
                DeAttach();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out AttachableCube attachable))
            {
                if (_attached)
                {
                    attachable.Attach(_cubeHolder);
                }
            }
        }

        public void Attach(CubeHolder cubeHolder)
        {
            if (!_attached)
            {
                _attached = true;
                _cubeHolder = cubeHolder;

                _cubeHolder.OnCubeAttached(_transform.localScale.y + .1f);
                _transform.parent = _cubeHolder.transform;
                SetPosition(_cubeHolder);
            }
        }

        public void DeAttach()
        {
            _attached = false;
            _transform.parent = null;

            ResetObject();
        }

        private void SetPosition(CubeHolder cubeHolder)
        {
            int childAmount = cubeHolder.transform.childCount;

            _transform.localPosition = new Vector3(
                0f,
                _colliderYBounds * childAmount,
                0f);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, _collisionLineLength));
        }

        protected override async void ResetObject()
        {
            await Task.Delay(_resetCubeDelayInSeconds * 1000);

            ReturnToPool(this);
        }
    }
}
