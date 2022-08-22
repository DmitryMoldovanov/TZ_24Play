using System;
using System.Threading.Tasks;
using Assets.Scripts.Cube;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Platform
{
    public class PlatformController : PooledObject<PlatformController>
    {
        private Action _onEndOfPlatformTrigger;

        [SerializeField] private PlatformCubesHolder _platformCubesHolder;
        [SerializeField] private EndOfPlatformTrigger _platformExit;
        [SerializeField] private float _moveLocalYTime;
    
        private Transform _transform;
        private readonly int _resetPlatformDelayInSeconds = 3;
        private readonly float _startYPosition = -60f;
        private readonly float _platformScaleZ = 30f;
    
        void Awake()
        {
            _transform = transform;
        }

        public void InitializePlatform(int platformIndex,
            Action onEndOfPlatformTrigger,
            ObjPool<AttachableCube> attachableCubesPool)
        {
            _transform.position = new Vector3(0, _startYPosition, _platformScaleZ * platformIndex);
            _platformCubesHolder.InitializePlatformCubesHolder(attachableCubesPool);
            _platformCubesHolder.PlaceCubesOnPlatform();

            LeanTween.moveLocalY(gameObject, 0f, _moveLocalYTime).setEaseOutBack();

            _onEndOfPlatformTrigger = onEndOfPlatformTrigger;

            _platformExit.OnPlatformTriggerEnterEvent += _onEndOfPlatformTrigger;
            _platformExit.OnPlatformTriggerEnterEvent += ResetObject;
        }

        protected override async void ResetObject()
        {
            _platformExit.OnPlatformTriggerEnterEvent -= _onEndOfPlatformTrigger;
            _platformExit.OnPlatformTriggerEnterEvent -= ResetObject;

            await Task.Delay(_resetPlatformDelayInSeconds * 1000);
            
            ReturnToPool(this);
        }
    }
}
