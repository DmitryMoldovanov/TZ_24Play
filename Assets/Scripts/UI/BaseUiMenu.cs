using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class BaseUiMenu : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
