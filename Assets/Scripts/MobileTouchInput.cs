using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class MobileTouchInput : IInputHandler
    {
        private readonly float _speed;
        private Touch _touch;
        private float _touchValue;
        private Vector3 _position;

        public MobileTouchInput(float speed)
        {
            _speed = speed;
            _touchValue = 0f;
            _position = Vector3.zero;
        }

        public float GetHorizontalInputData()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Moved)
                {
                    _touchValue = _touch.deltaPosition.x * _speed * Time.fixedDeltaTime;
                    _position.x += _touchValue;
                }
            }
            else _position.x = 0f;

            return _position.x;
        }
    }
}