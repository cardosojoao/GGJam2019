using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Attic.Decorations
{
    public enum DecorationState
    {
        Good,
        TurningGood,
        Evil
    }

    public enum DecorationType
    {
        Painting,
        FireIron,
        Chair,
        Belt,
    }

    [ExecuteInEditMode]
    public class DecorationObject : MonoBehaviour
    {
        public Animator DecorationAnimator;
        public Transform EvilDecoration;
        public Transform GoodDecoration;
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
            var stateDictionary = GameManager.Instance.DecorationManager.DecorationStateDictionary;
            if (stateDictionary.ContainsKey(DecorationType))
            {
                State = stateDictionary[DecorationType];
            }
        }

        private void SetState()
        {
            if (Application.isPlaying && gameObject.activeInHierarchy && _state == DecorationState.TurningGood)
            {
                StartCoroutine(Transition.WaitForTransition(() =>
                {
                    DecorationAnimator.SetTrigger("Change State");
                    _state = DecorationState.Good;
                }));
            }
            else
                TriggerSpriteChange();
        }

        public void TriggerSpriteChange()
        {
            switch (_state)
            {
                case (DecorationState.Evil):
                    EvilDecoration.gameObject.SetActive(true);
                    GoodDecoration.gameObject.SetActive(false);
                    break;
                case (DecorationState.Good):
                case (DecorationState.TurningGood):
                    EvilDecoration.gameObject.SetActive(false);
                    GoodDecoration.gameObject.SetActive(true);
                    break;
            }
        }

#if UNITY_EDITOR

        private void Update()
        {
            //if (!Application.isPlaying)
            //TriggerSpriteChange();
        }
#endif
    }
}
