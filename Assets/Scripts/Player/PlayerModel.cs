using UnityEngine;

namespace Assets.Scripts.Player
{

    [CreateAssetMenu(fileName = "PlayerModel", menuName = "ScriptableObjects/Player")]
    public class PlayerModel : ScriptableObject
    {
        [SerializeField]
        private float leftBorderLimit;

        [SerializeField]
        private float rightBorderLimit;

        [SerializeField]
        [Range(1f, 10f)]
        private float surfSpeed;

        [SerializeField]
        [Range(.1f, 1f)]
        private float horizontalSpeed;

        public float LeftBorderLimit => leftBorderLimit;
        public float RightBorderLimit => rightBorderLimit;
        public float SurfSpeed => surfSpeed;
        public float HorizontalSpeed => horizontalSpeed;
    }
}
