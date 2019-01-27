using UnityEngine;

namespace Assets.Scripts.UI.Controls
{
    public class MemoryControlsPanel : MonoBehaviour
    {
        public Animator Fader;
        private bool _fading;
        private void Update()
        {
            if (_fading || Transition.Instance.MidTransition)
                return;

            var axis = Input.GetAxisRaw("Horizontal");
            if (axis != 0f)
            {
                _fading = true;
                Fader.SetTrigger("Fade");
            }
        }

        private void FinishedFading()
        {
            gameObject.SetActive(false);
        }
    }
}
