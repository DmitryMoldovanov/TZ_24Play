using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadLevel(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}