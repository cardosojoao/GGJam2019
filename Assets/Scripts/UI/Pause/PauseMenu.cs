using Assets.Scripts.Util;
using UnityEngine;

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

        public void TogglePanel()
        {
            if (PanelTransform.gameObject.activeSelf)
                ClosePanel();
            else
                OpenPanel();
        }


        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                TogglePanel();
            }
        }
    }
}
