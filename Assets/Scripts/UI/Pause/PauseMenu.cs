using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI.Pause
{
    public class PauseMenu : SingletonMonoBehaviour<PauseMenu>
    {
        public Transform PanelTransform;

        public void OpenPanel()
        {
            Time.timeScale = 0f;
            PanelTransform.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            Time.timeScale = 1f;
            PanelTransform.gameObject.SetActive(false);
        }

        public void ToggleObject()
        {
            PanelTransform.gameObject.SetActive(!PanelTransform.gameObject.activeSelf);
        }
    }
}
