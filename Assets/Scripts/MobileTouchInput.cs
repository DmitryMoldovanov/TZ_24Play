using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class MobileTouchInput : IInputHandler
    {
        private readonly float _speed;
        private Touch _touch;
        private float _touchValue;
        private float _x;

        public MobileTouchInput(float speed)
        {
            _speed = speed;
            _touchValue = 0f;
            _x = 0f;
        }

        public float GetHorizontalInputData()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Moved)
                {
                    _touchValue = _touch.deltaPosition.x;
                    _x += _touchValue;
                }
            }
            else _x = 0f;

            return _x;
        }
    }
}