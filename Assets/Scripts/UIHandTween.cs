using UnityEngine;

public class UIHandTween : MonoBehaviour
{
    [SerializeField] private float _timeToMoveLocalX;
    private RectTransform _rectTransform;
    private float _moveToPosition;
    
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _moveToPosition = Screen.width / 2 - _rectTransform.rect.width;
    }

    void Start()
    {
        _rectTransform.localPosition.Set(
            _moveToPosition,
            _rectTransform.localPosition.y,
            _rectTransform.localPosition.z);

        LeanTween.moveLocalX(
            gameObject,
            _rectTransform.localPosition.x - _moveToPosition * 2,
            _timeToMoveLocalX).setLoopPingPong();
    }
}
