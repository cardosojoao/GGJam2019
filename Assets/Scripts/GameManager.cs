using Assets.Scripts.Util;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : PersistentMonoBehaviour<GameManager>
    {
        public void OpenScene(string nextScene, Func<IEnumerator> callback = null)
        {
            StartCoroutine(WaitForSceneLoad(nextScene, callback));
        }

        private IEnumerator WaitForSceneLoad(string nextScene, Func<IEnumerator> callback)
        {
            yield return SceneManager.LoadSceneAsync(nextScene);
            if (callback != null)
                yield return callback();
        }
    }
}
