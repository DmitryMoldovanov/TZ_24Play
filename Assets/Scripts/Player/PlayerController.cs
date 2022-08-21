using System;
using Assets.Scripts.Cube;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnPlayerDeathEvent;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private ParticleSystem _cubeAttachmentParticles;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private CubeHolder _cubeHolder;

        private IInputHandler _inputHandler;
        private CollisionCaster _collisionCaster;

        private Transform _transform;
        private Vector3 _position;
        private float _inputX;

        #region MONO

        void Awake()
        {
            _transform = transform;
            _inputHandler = new MobileTouchInput(_playerModel.HorizontalSpeed);
            _collisionCaster =
                new CollisionCaster(_transform, _playerModel.CollisionDistance, _playerModel.CollisionMask);
            _position = Vector3.forward;
        }

        private void OnEnable()
        {
            _rigidbody.isKinematic = false;
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
            if (!_collisionCaster.HasCollided())
            {
                Move();
            } 
            else OnPlayerDeathEvent?.Invoke();
        }

        private void Move()
        {
            _rigidbody.MovePosition(new Vector3(
                _position.x,
                _transform.position.y,
                _transform.position.z + _position.z * _playerModel.SurfSpeed * Time.fixedDeltaTime));
        }

        private void ReadInput()
        {
            _inputX = _inputHandler.GetHorizontalInputData();

            _position.x = Mathf.Clamp(
                _transform.position.x + (_inputX * _playerModel.HorizontalSpeed * Time.fixedDeltaTime),
                _playerModel.LeftBorderLimit,
                _playerModel.RightBorderLimit);
        }

        private void AlignPlayerPosition(float y)
        {
            PlayJumpAnimation();
            _cubeAttachmentParticles.Play();

            _transform.position = new Vector3(
                _rigidbody.position.x,
                _rigidbody.position.y + y,
                _rigidbody.position.z);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IAttachable attachable))
            {
                attachable.Attach(_cubeHolder);
            }
        }

        private void PlayJumpAnimation()
        {
            _playerAnimator.SetTrigger("Jump");
        }
    }
}