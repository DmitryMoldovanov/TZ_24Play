using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private int _shakeDurationInMilliseconds;

        private CinemachineVirtualCamera _camera;

        void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        public async Task ShakeCamera()
        {
            var noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = _shakeIntensity;
            await Task.Delay(_shakeDurationInMilliseconds);
            noise.m_AmplitudeGain = 0;
        }
    }
}