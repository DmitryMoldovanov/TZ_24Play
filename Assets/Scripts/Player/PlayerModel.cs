using UnityEngine;

namespace Assets.Scripts.Player
{

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "ScriptableObjects/Player")]
    public class PlayerModel : ScriptableObject
    {
        [SerializeField] private float _leftBorderLimit;

        [SerializeField] private float _rightBorderLimit;

        [SerializeField]
        [Range(1f, 15f)] private float _surfSpeed;

        [SerializeField]
        [Range(.1f, 1f)] private float _horizontalSpeed;

        [SerializeField] private LayerMask _collisionMask;

        [SerializeField]
        [Range(.1f, 2f)] private float _collisionDistance;

        public float LeftBorderLimit => _leftBorderLimit;
        public float RightBorderLimit => _rightBorderLimit;
        public float SurfSpeed => _surfSpeed;
        public float HorizontalSpeed => _horizontalSpeed;
        public LayerMask CollisionMask => _collisionMask;
        public float CollisionDistance => _collisionDistance;
    }
}
