using System.Threading.Tasks;
using Assets.Scripts.Cube;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Platform
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private PlatformController[] _platformControllerPrefabs;
        [SerializeField] private AttachableCube _attachableAttachableCubePrefab;
        [SerializeField] private int _spawnPlatformsOnGameStart;

        private ObjPool<PlatformController> _platformsPool;
        private ObjPool<AttachableCube> _attachableCubesPool;

        private int _platformIndex;

        #region MONO

        void Awake()
        {
            _platformsPool = new ObjPool<PlatformController>(
                _platformControllerPrefabs,
                transform,
                _spawnPlatformsOnGameStart,
                true);

            _attachableCubesPool = new ObjPool<AttachableCube>(
                _attachableAttachableCubePrefab,
                transform,
                _spawnPlatformsOnGameStart * _platformControllerPrefabs.Length,
                true);

            _platformIndex = 0;
        }
        
        private async void Start()
        {
            await SpawnStartingPlatform(_spawnPlatformsOnGameStart);
        }
        
        #endregion

        private void SpawnRandomPlatform()
        {
            PlatformController platformController = _platformsPool.Pool.Get();
            platformController.InitializePlatform(_platformIndex, SpawnRandomPlatform, _attachableCubesPool);
            _platformIndex++;
        }

        private async Task SpawnStartingPlatform(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                SpawnRandomPlatform();
                await Task.Delay(100);
            }
        }
    }
}