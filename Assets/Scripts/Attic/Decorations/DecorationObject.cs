using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Attic.Decorations
{
    public enum DecorationState
    {
        Good,
        Evil
    }

    public enum DecorationType
    {
        Painting,
        Chair,
        Belt,
        Window
    }

    [ExecuteInEditMode]
    public class DecorationObject : MonoBehaviour
    {
        public SpriteRenderer DecorationRenderer;
        public Sprite EvilDecoration;
        public Sprite GoodDecoration;
        public DecorationType DecorationType;
        [SerializeField]
        private DecorationState _state;
        public DecorationState State
        {
            get { return _state; }
            set
            {
                _state = value;
                SetState();
            }
        }

        private void Start()
        {
            StartCoroutine(WaitForManager());
        }

        private IEnumerator WaitForManager()
        {
            while (GameManager.Instance == null)
                yield return null;

            SetDecorationState();
        }

        private void SetDecorationState()
        {
            var stateDictionary = GameManager.Instance.DecorationManager.DecorationState;
            if (stateDictionary.ContainsKey(DecorationType))
            {
                State = stateDictionary[DecorationType];
            }
        }

        private void SetState()
        {
            Sprite targetSprite = null;
            switch (_state)
            {
                case (DecorationState.Evil):
                    targetSprite = EvilDecoration;
                    break;
                case (DecorationState.Good):
                    targetSprite = GoodDecoration;
                    break;
            }

            DecorationRenderer.sprite = targetSprite;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
                SetState();
        }
#endif
    }
}
