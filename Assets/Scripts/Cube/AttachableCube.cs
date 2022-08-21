using System.Threading.Tasks;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Cube
{
    public class AttachableCube : PooledObject<AttachableCube>, IAttachable
    {
        [SerializeField] private bool _isAttached;
        [SerializeField] private CubeHolder _cubeHolder;
        [SerializeField] private LayerMask _layerToDeAttach;
        [SerializeField] private float _collisionLineLength;

        private readonly int _resetCubeDelayInSeconds = 3;

        private CollisionCaster _collisionCaster;
        private Transform _transform;
        private float _colliderYBounds;
        private bool _enteredCollision;

        public bool EnteredCollision => _enteredCollision;

        void Awake()
        {
            _transform = transform;
            _collisionCaster = new CollisionCaster(_transform, _collisionLineLength, _layerToDeAttach);
            _colliderYBounds = GetComponent<BoxCollider>().size.y;
        }

        public void Initialize(Transform parent, Vector3 position)
        {
            _transform.parent = parent;
            _transform.localPosition = position;
        }

        private void FixedUpdate()
        {
            if (_isAttached && _collisionCaster.HasCollided())
            {
                DeAttach();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isAttached)
            {
                if (collision.transform.TryGetComponent(out IAttachable attachable) &&
                    !attachable.EnteredCollision)
                {
                    attachable.Attach(_cubeHolder);
                }
            }
        }

        public void Attach(CubeHolder cubeHolder)
        {
            if (!_isAttached)
            {
                _isAttached = true;
                _enteredCollision = true;
                _cubeHolder = cubeHolder;

                _cubeHolder.OnCubeAttached(_colliderYBounds);
                _transform.parent = _cubeHolder.transform;
                SetPosition(_cubeHolder);
            }
        }

        private void SetPosition(CubeHolder cubeHolder)
        {
            int childAmount = cubeHolder.transform.childCount;

            _transform.localPosition = new Vector3(
                0,
                -1f * childAmount,
                0);
        }

        public void DeAttach()
        {
            _cubeHolder.OnCubeDeAttached();
            _isAttached = false;
            _transform.parent = null;

            ResetObject();
        }

        protected override async void ResetObject()
        {
            await Task.Delay(_resetCubeDelayInSeconds * 1000);
            _enteredCollision = false;

            ReturnToPool(this);
        }
    }
}