using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : PersistentMonoBehaviour<GameManager>
    {
        public Dictionary<DecorationType, DecorationState> DecorationState = new Dictionary<DecorationType, DecorationState>();


        public void ResetGame()
        {
            DecorationState.Clear();
        }

        public void OpenScene(string nextScene, Func<IEnumerator> callback = null)
        {
            StartCoroutine(WaitForSceneLoad(nextScene, callback));
        }

        private IEnumerator WaitForSceneLoad(string nextScene, Func<IEnumerator> callback)
        {
            if (Transition.Instance != null)
                yield return Transition.Instance.StartTransition();
            yield return SceneManager.LoadSceneAsync(nextScene);
            if (Transition.Instance != null)
                Transition.Instance.EndTransition();
            if (callback != null)
                yield return callback();
        }
    }
}
