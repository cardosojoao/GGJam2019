using UnityEngine;

namespace Assets.Scripts.UI.Pause
{
    public class ToMainMenuButton : MonoBehaviour
    {
        public string MainMenuScene;

        public void ToMainMenu()
        {
            Time.timeScale = 1f;
            GameManager.Instance.OpenScene(MainMenuScene);
            GameManager.Instance.ResetGame();
        }
    }
}
