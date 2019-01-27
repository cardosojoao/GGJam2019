using Assets.Scripts.Util;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverScreen : SingletonMonoBehaviour<GameOverScreen>
    {
        public Transform GameOverContainer;
        public Animator GameOverAnimator;
        public bool Active = false;
        public bool CanSkip = false;
        public AudioClip EndMusic;
        public float EndMusicVolume;

        public void TriggerGameOver()
        {
            Active = true;
            GameOverContainer.gameObject.SetActive(true);
            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            GameManager.Instance.MusicManager.SetMusic(EndMusic, EndMusicVolume);
            yield return Transition.WaitForTransition();

            GameOverAnimator.SetTrigger("StartGameOver");
        }

        public void GameOverAnimationEnded()
        {
            CanSkip = true;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Activate") && CanSkip)
            {
                GameManager.Instance.ResetGame();
                GameManager.Instance.OpenScene("MainMenu");
            }
        }
    }
}
