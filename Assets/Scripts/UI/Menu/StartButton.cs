using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class StartButton : MonoBehaviour
    {
        public string NextScene = "Attic";

        public void StartPlay()
        {
            GameManager.Instance.OpenScene(NextScene);
        }
    }
}
