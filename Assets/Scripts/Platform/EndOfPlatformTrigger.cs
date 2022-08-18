using System;
using Assets.Scripts.Cube;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Platform
{
    public class EndOfPlatformTrigger : MonoBehaviour
    {
        public event Action OnPlatformTriggerEnterEvent;

        private bool _triggered;

        void OnEnable()
        {
            _triggered = false;
        }

        void OnDisable()
        {
            _triggered = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_triggered == false)
            {
                if (collider.transform.TryGetComponent(out MainCube mainCube) ||
                    collider.transform.TryGetComponent(out IAttachable attachable))
                {
                    _triggered = true;
                    OnPlatformTriggerEnterEvent?.Invoke();
                }
            }
        }
    }
}
