using UnityEngine;

namespace Assets.Scripts
{
    public class CollisionCaster
    {
        private Transform _transform;
        private float _collisionDistance;
        private LayerMask _layerMask;

        private float _HalfExtentsX;
        private float _HalfExtentsY;

        public CollisionCaster(Transform transform, float collisionDistance, LayerMask layerMask)
        {
            _transform = transform;
            _collisionDistance = collisionDistance;
            _layerMask = layerMask;

            CalculateExtents();
        }

        public bool HasCollided()
        {
            var isHit = Physics.BoxCast(
                _transform.position,
                new Vector3(_HalfExtentsX, _HalfExtentsY, _collisionDistance),
                _transform.forward,
                Quaternion.identity,
                _collisionDistance,
                _layerMask.value);
            
            return isHit;
        }

        private void CalculateExtents()
        {
            _HalfExtentsX = _transform.localScale.x / 2 - 0.1f;
            _HalfExtentsY = _transform.localScale.y / 2 - 0.1f;
        }

        private void OnDrawGizmos()
        {
            if (HasCollided())
            {
                Gizmos.color = Color.green;

                Gizmos.DrawWireCube(
                    _transform.position,
                    new Vector3(_HalfExtentsX, _HalfExtentsY, _collisionDistance));
            }
        }
    }
}
