using Assets.Scripts.Attic.Decorations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Memory
{
    [Serializable]
    public struct MemorySliceSprite
    {
        public DecorationType Decoration;
        public Sprite[] SpriteArray;
    }

    public class MemoryReel : MonoBehaviour
    {
        public MemorySliceSprite[] MemorySpriteArray;

        private Dictionary<DecorationType, Sprite[]> p_memorySpriteDictionary;
        private Dictionary<DecorationType, Sprite[]> _memorySpriteDictionary
        {
            get
            {
                if (p_memorySpriteDictionary == null)
                {
                    p_memorySpriteDictionary = new Dictionary<DecorationType, Sprite[]>();
                    if (MemorySpriteArray != null)
                    {
                        foreach (MemorySliceSprite sliceSprite in MemorySpriteArray)
                        {
                            p_memorySpriteDictionary[sliceSprite.Decoration] = sliceSprite.SpriteArray;
                        }
                    }
                }
                return p_memorySpriteDictionary;
            }
        }
        public MemorySlice[] SliceArray;

        [SerializeField]
        private DecorationType _currentMemoryReel;
        public DecorationType CurrnetMemoryReel
        {
            get { return _currentMemoryReel; }
            set
            {
                _currentMemoryReel = value;
                SetMemoryReel();
            }
        }


        public void SetMemoryReel()
        {
            if (!_memorySpriteDictionary.ContainsKey(_currentMemoryReel) || SliceArray == null)
                return;

            Sprite[] spriteArray = _memorySpriteDictionary[_currentMemoryReel];
            for (int i = 0; i < Mathf.Min(spriteArray.Length, SliceArray.Length); i++)
            {
                SliceArray[i].SliceImage.sprite = spriteArray[i];
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying)
                SetMemoryReel();
        }
#endif
    }
}
