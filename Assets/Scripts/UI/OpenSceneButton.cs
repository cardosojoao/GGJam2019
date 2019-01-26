using UnityEngine;

namespace Assets.Scripts.UI
{
    public class OpenSceneButton : MonoBehaviour
    {
        public string TargetScene;

        public void OpenScene()
        {
            GameManager.Instance.OpenScene(TargetScene);
        }
    }
}
