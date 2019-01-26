using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : PersistentMonoBehaviour<GameManager>
    {
        public DecorationManager DecorationManager;

        private void Start()
        {
            StartCoroutine(CheckIfController());
        }

        public bool Paused { get; set; }
        public bool HasController { get; set; }

        public void ResetGame()
        {
            DecorationManager.Clear();
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


        private IEnumerator CheckIfController()
        {
            var waitForSecond = new WaitForSecondsRealtime(1f);
            while (true)
            {
                HasController = HasConnectedJoystick();
                yield return waitForSecond;
            }
        }


        private bool HasConnectedJoystick()
        {
            var joystickNameArray = Input.GetJoystickNames();
            if (joystickNameArray == null || joystickNameArray.Length == 0)
                return false;

            foreach (string joystickName in joystickNameArray)
            {
                if (!string.IsNullOrWhiteSpace(joystickName))
                    return true;
            }
            return false;
        }
    }
}
