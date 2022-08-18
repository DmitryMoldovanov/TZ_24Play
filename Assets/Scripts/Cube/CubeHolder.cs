using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Cube
{
    public class CubeHolder : MonoBehaviour
    {
        public event Action<float> OnCubeAttachEvent;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void OnCubeAttached(float addValueToPlayerY)
        {
            OnCubeAttachEvent?.Invoke(addValueToPlayerY);
        }
    }
}
