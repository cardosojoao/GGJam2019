using UnityEngine;

namespace Assets.Scripts.UI
{
    public class OpenSceneButton : MonoBehaviour
    {
        public string TargetScene;
        public bool CheckForActivateButton;

        public void OpenScene()
        {
            GameManager.Instance.OpenScene(TargetScene);
        }

        private void Update()
        {
            if (CheckForActivateButton && Input.GetButtonDown("Activate"))
                OpenScene();
        }
    }
}
