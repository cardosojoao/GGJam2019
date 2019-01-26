using Assets.Scripts.Util;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Transition : PersistentMonoBehaviour<Transition>
    {
        public Animator TransitionAnimator;
        public Transform TransitionContainer;

        public bool TransitionIn { get; private set; }

        public IEnumerator StartTransition()
        {
            TransitionContainer.gameObject.SetActive(true);
            TransitionAnimator.SetTrigger("Transition In");
            TransitionIn = true;
            while (TransitionIn)
                yield return null;
        }

        public void TransitionInEnded()
        {
            TransitionIn = false;
        }

        public void EndTransition()
        {
            TransitionAnimator.SetTrigger("Transition Out");
        }

        public void TransitionEnded()
        {
            TransitionContainer.gameObject.SetActive(false);
        }
    }
}
