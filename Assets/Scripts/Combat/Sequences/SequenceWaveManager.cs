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

        private bool WavesActive = false;
        private int _timeInDif;
        private WaveDifficulty Dificulty
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
            SequenceManager.ClearSequence();
        }

        private IEnumerator WaveRoutine()
        {
            WavesActive = true;
            yield return SequenceManager.NextSequence(Dificulty.SequenceSize, WaveFinished);
            if (Dificulty.SequenceExpireTime > 0f)
            {
                yield return new WaitForSeconds(Dificulty.SequenceExpireTime);
                yield return WaveRoutine();
            }
        }

        private void WaveFinished(bool success)
        {
            if (WavesActive && (success || Dificulty.SequenceExpireTime == 0))
            {
                if (success)
                    _timeInDif++;

                StopAllCoroutines();
                StartCoroutine(WaveRoutine());

                if (_timeInDif >= Dificulty.RoundToNextDif)
                    GameManager.Instance.Dificulty++;
            }
        }
    }
}
