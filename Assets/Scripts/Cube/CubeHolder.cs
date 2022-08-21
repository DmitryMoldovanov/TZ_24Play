using System;
using UnityEngine;

namespace Assets.Scripts.Cube
{
    public class CubeHolder : MonoBehaviour
    {
        public event Action<float> OnCubeAttachEvent;

        [SerializeField] private CameraShake _camera;

        private Transform _transform;
        private bool _isShakingCamera;

        private void Awake()
        {
            _transform = transform;
        }

        public void OnCubeAttached(float addValueToPlayerY)
        {
            OnCubeAttachEvent?.Invoke(addValueToPlayerY);
        }

        public async void OnCubeDeAttached()
        {
            if (!_isShakingCamera)
            {
                _isShakingCamera = true;
                await _camera.ShakeCamera();
                _isShakingCamera = false;
            }
        }
    }
}
