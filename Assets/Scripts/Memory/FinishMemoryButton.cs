using Assets.Scripts.Attic.Decorations;
using UnityEngine;

namespace Assets.Scripts.Memory
{
    public class FinishMemoryButton : MonoBehaviour
    {
        public string TargetScene;
        public MemoryReel MemoryReel;

        public void OpenScene()
        {
            var currentMemory = MemoryReel.CurrentMemoryReel;

            GameManager.Instance.DecorationManager.SetDecorationState(currentMemory, DecorationState.TurningGood);
            GameManager.Instance.OpenScene(TargetScene);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Activate") && !GameManager.Instance.Paused)
                OpenScene();
        }
    }
}
