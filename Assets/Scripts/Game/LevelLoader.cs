using Assets.Scripts.Platform;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadLevel(string sceneName)
        {
            var progress = SceneManager.LoadSceneAsync(sceneName);
            progress.allowSceneActivation = false;
            
            while (progress.progress < 0.9f)
            {
                Debug.Log("Loading...");
            }
            progress.allowSceneActivation = true;
        }
    }
}