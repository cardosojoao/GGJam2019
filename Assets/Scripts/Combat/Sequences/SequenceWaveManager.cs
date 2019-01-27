using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Sequences
{
    [Serializable]
    public struct WaveDifficulty
    {
        public int SequenceSize;
        public float SequenceExpireTime;
        public int RoundToNextDif;
    }

    public class SequenceWaveManager : MonoBehaviour
    {
        public WaveDifficulty[] WaveDifficultyArray;
        public SequenceManager SequenceManager;
        public RefreshBar RefreshBar;

        private bool WavesActive = false;
        private int _timeInDif;
        private WaveDifficulty Difficulty
        {
            get
            {
                var dif = Mathf.Clamp(GameManager.Instance.Dificulty, 0, WaveDifficultyArray.Length - 1);
                return WaveDifficultyArray[dif];
            }
        }


        public void StartWaves()
        {
            /*
            if (WaveDifficultyArray == null || levelDificulty >= WaveDifficultyArray.Length)
            {
                Debug.Log("Wave difficulty not set (" + levelDificulty + ")");
                return;
            }
            */

            if (WavesActive)
                StopWaves();

            StopAllCoroutines();
            StartCoroutine(WaveRoutine());
        }

        public void StopWaves()
        {
            WavesActive = false;
            StopAllCoroutines();
            SequenceManager.ClearSequence();
        }

        private IEnumerator WaveRoutine()
        {
            WavesActive = true;
            yield return SequenceManager.NextSequence(Difficulty.SequenceSize, WaveFinished);

            if (Difficulty.SequenceExpireTime > 0f)
            {
                yield return WaitForRefresh();
                yield return WaveRoutine();
            }
        }

        private IEnumerator WaitForRefresh()
        {
            var startTime = Time.time;
            var timePassed = Time.time - startTime;
            var expireTime = Difficulty.SequenceExpireTime;
            RefreshBar.Refresh();
            while (timePassed < expireTime)
            {
                RefreshBar.SetState(expireTime - timePassed, expireTime);
                yield return null;
                timePassed = Time.time - startTime;
            }

            RefreshBar.SetState(0, expireTime);

        }

        private void WaveFinished(bool success)
        {
            if (WavesActive && (success || Difficulty.SequenceExpireTime == 0))
            {
                if (success)
                    _timeInDif++;

                if (_timeInDif >= Difficulty.RoundToNextDif)
                {
                    GameManager.Instance.Dificulty++;
                    _timeInDif = 0;
                }

                StopAllCoroutines();
                StartCoroutine(WaveRoutine());

            }
        }
    }
}
