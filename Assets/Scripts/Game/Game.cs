using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        
        [Space]
        [Header("UI")]
        [SerializeField] private StartGameMenu _startGameMenu;
        [SerializeField] private EndOfGameMenu _endOfGameMenu;

        void OnEnable()
        {
            _player.OnPlayerDeathEvent += EndOfGame;
        }

        void OnDisable()
        {
            _player.OnPlayerDeathEvent -= EndOfGame;
        }

        public void StartGame()
        {
            _startGameMenu.Disable();
            _player.enabled = true;
        }

        private void EndOfGame()
        {
            _endOfGameMenu.Enable();
            _player.enabled = false;
        }
    }
}
