using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Pause
{
    public class PauseMenu : SingletonMonoBehaviour<PauseMenu>
    {
        public Transform PanelTransform;
        public Selectable FirstSelectedButton;

        public void OpenPanel()
        {
            Time.timeScale = 0f;
            FirstSelectedButton.Select();
            GameManager.Instance.Paused = true;
            PanelTransform.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            Time.timeScale = 1f;
            GameManager.Instance.Paused = false;
            PanelTransform.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
            if (GameManager.Instance != null)
                GameManager.Instance.Paused = false;
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
            if (Input.GetButtonDown("Pause"))
            {
                TogglePanel();
            }
        }
    }
}
