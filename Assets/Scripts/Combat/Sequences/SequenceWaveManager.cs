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
    }

    public class SequenceWaveManager : MonoBehaviour
    {
        public WaveDifficulty[] WaveDifficultyArray;
        public SequenceManager SequenceManager;

        private WaveDifficulty _currentDifficulty;
        private bool WavesActive = false;

        public void StartWaves(int levelDificulty)
        {
            if (WaveDifficultyArray == null || levelDificulty >= WaveDifficultyArray.Length)
            {
                Debug.Log("Wave difficulty not set (" + levelDificulty + ")");
                return;
            }

            if (WavesActive)
                StopWaves();

            _currentDifficulty = WaveDifficultyArray[levelDificulty];
            StopAllCoroutines();
            StartCoroutine(WaveRoutine(_currentDifficulty));
        }

        public void StopWaves()
        {
            WavesActive = false;
            SequenceManager.ClearSequence();
        }

        private IEnumerator WaveRoutine(WaveDifficulty difficulty)
        {
            WavesActive = true;
            SequenceManager.NextSequence(difficulty.SequenceSize, WaveFinished);
            if (difficulty.SequenceExpireTime > 0f)
            {
                yield return new WaitForSeconds(difficulty.SequenceExpireTime);
                SequenceManager.ClearSequence();
            }
        }

        private void WaveFinished(bool success)
        {
            if (WavesActive)
            {
                StopAllCoroutines();
                StartCoroutine(WaveRoutine(_currentDifficulty));
            }
        }
    }
}
