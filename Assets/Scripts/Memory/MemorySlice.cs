﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Memory
{
    public class MemorySlice : MonoBehaviour
    {
        public Image SliceImage;
        public RectTransform RectTransform;
        public LayoutElement LayoutElement;
        public Animator SliceAnimator;

        [SerializeField]
        private bool _revealed;
        public bool Revealed
        {
            get { return _revealed; }
            set
            {
                if (_revealed && value)
                    return;

                _revealed = value;
                SetRevealed();
            }
        }

        private void SetRevealed()
        {

            gameObject.SetActive(_revealed);
            if (_revealed && gameObject.activeInHierarchy)
                SliceAnimator.SetTrigger("Reveal");
        }

    }
}
