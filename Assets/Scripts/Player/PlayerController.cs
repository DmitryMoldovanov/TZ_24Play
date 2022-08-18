using Assets.Scripts.Cube;
using Assets.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private MainCube _mainCube;
        [SerializeField] private ParticleSystem _cubeAttachmentParticles;
        [SerializeField] private Transform _playerPrefab;
        [SerializeField] private CubeHolder _cubeHolder;

        private IInputHandler _inputHandler;
        private Transform _transform;
        private Transform _mainCubeTransform;
        private Animator _animator;
        private Vector3 _position;
        private float _x;

        public MainCube PlayerCube => _mainCube;

        #region MONO
        void Awake()
        {
            _transform = transform;
            _animator = _playerPrefab.transform.GetComponent<Animator>();
            _mainCubeTransform = _mainCube.transform;
            _inputHandler = new MobileTouchInput(_playerModel.HorizontalSpeed);
            _position = Vector3.forward;
        }

        private void OnEnable()
        {
            _cubeHolder.OnCubeAttachEvent += AlignPlayerPosition;
        }

        private void OnDisable()
        {
            _cubeHolder.OnCubeAttachEvent -= AlignPlayerPosition;
        }

        #endregion
        
        void Update()
        {
            ReadInput();
        }

        void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _transform.position = new Vector3(
                _position.x,
                _transform.position.y,
                _transform.position.z + _playerModel.SurfSpeed * Time.fixedDeltaTime);
        }

        private void ReadInput()
        {
            _x = _inputHandler.GetHorizontalInputData();
            _position.x = Mathf.Clamp(_transform.position.x + _x, _playerModel.LeftBorderLimit, _playerModel.RightBorderLimit);
        }

        private void AlignPlayerPosition(float y)
        {
            PlayJumpAnimation();
            
            _cubeAttachmentParticles.Play();

            _mainCubeTransform.localPosition = new Vector3(
                _mainCubeTransform.localPosition.x,
                _mainCubeTransform.localPosition.y + y,
                _mainCubeTransform.localPosition.z);
        }

        private void PlayJumpAnimation()
        {
            _animator.SetTrigger("Jump");
        }
    }
}