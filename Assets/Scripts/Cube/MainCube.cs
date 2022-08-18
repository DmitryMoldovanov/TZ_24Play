using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Cube
{
    public class MainCube : MonoBehaviour
    {
        public event Action OnPlayerDeathEvent;

        [SerializeField] private CubeHolder _cubeHolder;
        [SerializeField] private float _collisionLineLength;
        [SerializeField] private LayerMask _layerMaskToDieFrom;

        private Transform _transform;
        private bool _isAlive;

        void Awake()
        {
            _transform = transform;
            _isAlive = true;
        }

        private void FixedUpdate()
        {
            if (_isAlive && Physics.Linecast(
                    _transform.position,
                    _transform.position + new Vector3(0, 0, _collisionLineLength),
                    _layerMaskToDieFrom.value))
            {
                _isAlive = false;
                OnPlayerDeathEvent?.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IAttachable attachable))
            {
                attachable.Attach(_cubeHolder);
            }
        }
    }
}