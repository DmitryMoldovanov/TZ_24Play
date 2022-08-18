using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerPrefab;
        
        [Space]
        [Header("UI")]
        [SerializeField] private StartGameMenu _startGameMenu;
        [SerializeField] private EndOfGameMenu _endOfGameMenu;


        void Awake()
        {
            
        }

        void OnEnable()
        {
            _playerPrefab.PlayerCube.OnPlayerDeathEvent += EndOfGame;
        }

        void OnDisable()
        {
            _playerPrefab.PlayerCube.OnPlayerDeathEvent -= EndOfGame;
        }

        void Start()
        {

        }

        public void StartGame()
        {
            _startGameMenu.Disable();
            _playerPrefab.enabled = true;
        }

        private void EndOfGame()
        {
            _endOfGameMenu.Enable();
            _playerPrefab.enabled = false;
        }
    }
}
