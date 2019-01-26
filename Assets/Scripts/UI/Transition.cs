using Assets.Scripts.Util;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Transition : PersistentMonoBehaviour<Transition>
    {
        public Animator TransitionAnimator;
        public Transform TransitionContainer;

        public bool TransitionIn { get; private set; }
        public bool MidTransition { get; private set; }

        private Action _transitionCallback = null;

        public IEnumerator StartTransition()
        {
            MidTransition = true;
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

        public void EndTransition(Action callback = null)
        {
            _transitionCallback = callback;
            TransitionAnimator.SetTrigger("Transition Out");
        }

        public void TransitionEnded()
        {
            if (_transitionCallback != null)
                _transitionCallback();
            TransitionContainer.gameObject.SetActive(false);
            MidTransition = false;
        }

        public static IEnumerator WaitForTransition(Action callback = null)
        {
            while (Instance.MidTransition)
                yield return null;

            if (callback != null)
                callback();
        }
    }
}
