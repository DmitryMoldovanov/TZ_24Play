using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
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
        
        private void OnTriggerEnter(Collider collider)
        {
            if (_triggered == false)
            {
                if (collider.transform.TryGetComponent(out PlayerController player) ||
                    collider.transform.TryGetComponent(out IAttachable attachable))
                {
                    _triggered = true;
                    OnPlatformTriggerEnterEvent?.Invoke();
                }
            }
        }
    }
}