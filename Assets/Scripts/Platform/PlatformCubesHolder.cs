using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Cube;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Platform
{
    public class PlatformCubesHolder : MonoBehaviour
    {
        [SerializeField] private Transform[] _attachableCubes;

        private ObjPool<AttachableCube> _attachableCubesPool;
        private Vector3[] _cubeDefaultPositions;
        private List<Vector3> _existingCubes;
        private Transform _transform;

        void Awake()
        {
            _transform = transform;
            CacheDefaultPositions();
        }

        public void InitializePlatformCubesHolder(ObjPool<AttachableCube> attachableCubesPool)
        {
            _attachableCubesPool = attachableCubesPool;
        }

        public void PlaceCubesOnPlatform()
        {
            CheckIfPlatformIsNotEmpty();

            foreach (var position in _cubeDefaultPositions)
            {
                if (!_existingCubes.Contains(position))
                    _attachableCubesPool.Pool.Get().Initialize(_transform, position);
            }
            _existingCubes.Clear();
        }

        private void CheckIfPlatformIsNotEmpty()
        {
            var childAmount = _transform.childCount;

            if (childAmount > 3)
            {
                for (int i = 3; i < childAmount; i++)
                {
                    var localPos = _transform.GetChild(i).localPosition;

                    if (_cubeDefaultPositions.Contains(localPos))
                        _existingCubes.Add(localPos);
                }
            }
        }

        private void CacheDefaultPositions()
        {
            _cubeDefaultPositions = new Vector3[_attachableCubes.Length];
            _existingCubes = new List<Vector3>(_attachableCubes.Length);

            for (int i = 0; i < _attachableCubes.Length; i++)
            {
                _cubeDefaultPositions[i] = _attachableCubes[i].localPosition;
            }
        }
    }
}
