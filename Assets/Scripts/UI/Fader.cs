using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Fader : MonoBehaviour
    {
        public Animator FaderAnimator;

        public void FadeIn()
        {
            gameObject.SetActive(true);
            FaderAnimator.SetTrigger("Fade In");
        }

        public void FadeOut()
        {
            if (gameObject.activeInHierarchy)
                FaderAnimator.SetTrigger("Fade Out");
        }

        public void FadeOutFinished()
        {
            gameObject.SetActive(false);
        }
    }
}
