using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterObject : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(WaitForManager());
        }

        private IEnumerator WaitForManager()
        {
            while (GameManager.Instance == null)
                yield return null;

            var stateDictionary = GameManager.Instance.DecorationManager.DecorationStateDictionary;
            if (stateDictionary.ContainsKey(DecorationType.Chair))
            {
                var state = stateDictionary[DecorationType.Chair];
                if (state == DecorationState.Good || state == DecorationState.TurningGood)
                {
                    gameObject.SetActive(false);
                    GameOverScreen.Instance.TriggerGameOver();
                }
            }
        }
    }
}
