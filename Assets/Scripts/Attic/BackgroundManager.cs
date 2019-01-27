using Assets.Scripts.Attic.Decorations;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Attic
{
    public class BackgroundManager : MonoBehaviour
    {
        public SpriteRenderer BackgroundSpriteRenderer;
        public Sprite LightSprite;
        public Sprite DarkSprite;

        private void Start()
        {
            StartCoroutine(WaitForManager());
        }

        private IEnumerator WaitForManager()
        {
            while (GameManager.Instance == null)
                yield return null;

            SetBackgroundState();
        }

        private void SetBackgroundState()
        {
            var state = GameManager.Instance.DecorationManager;
            var decState = state.GetDecorationState(DecorationType.Chair);

            if (decState == DecorationState.Good || decState == DecorationState.TurningGood)
            {
                BackgroundSpriteRenderer.sprite = LightSprite;
            }
            else
            {
                BackgroundSpriteRenderer.sprite = DarkSprite;
            }
        }
    }
}
