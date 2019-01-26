using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Button
{
    public class ButtonSpriteChanger : MonoBehaviour
    {
        public Image SpriteImage;
        public SpriteRenderer SpriteRenderer;

        public Sprite KeyboardSprite;
        public Sprite ControllerSprite;

        private void Update()
        {
            SetSprite();
        }

        private void SetSprite()
        {
            Sprite targetSprite;
            if (GameManager.Instance.HasController)
                targetSprite = ControllerSprite;
            else
                targetSprite = KeyboardSprite;

            if (SpriteImage != null)
                SpriteImage.sprite = targetSprite;
            if (SpriteRenderer != null)
                SpriteRenderer.sprite = targetSprite;
        }

    }
}
