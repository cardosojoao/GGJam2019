using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Sequences
{
    public class Sequence
    {
        private string[] _currentSequence;
        private int _currentIndex;
        private SequenceButton[] _buttonArray;
        public int Count { get { return _buttonArray.Length; } }

        public Sequence(string[] sequence, SequenceButton[] buttonArray)
        {
            _buttonArray = buttonArray;
            _currentSequence = sequence;
            _currentIndex = 0;
        }

        public bool Broken { get; set; }
        public bool Finished { get { return _currentIndex >= _currentSequence.Length; } }
        public IEnumerator Clear()
        {
            yield return WaitForAnimationClear();
            foreach (SequenceButton button in _buttonArray)
            {
                button.SelfDestroyAnimation();
            }

            yield return WaitForAnimationClear();

            foreach (SequenceButton button in _buttonArray)
            {
                Object.Destroy(button.gameObject);
            }
        }

        private IEnumerator WaitForAnimationClear()
        {
            var clearing = true;
            while (clearing)
            {
                yield return new WaitForSeconds(0.2f);
                clearing = false;
                foreach (SequenceButton button in _buttonArray)
                {
                    if (button.Clearing)
                    {
                        clearing = true;
                        break;
                    }
                }
            }
        }

        public string CurrentButton()
        {
            if (_currentIndex >= _currentSequence.Length)
                return null;

            return _currentSequence[_currentIndex];
        }

        public bool InFinalButton()
        {
            return _currentIndex == _currentSequence.Length - 1;
        }

        public void ShowClickButton(bool correct)
        {
            if (correct)
                _buttonArray[_currentIndex].ClickedCorrect();
            else
            {
                foreach (SequenceButton button in _buttonArray)
                {
                    button.ClickWrong();
                }
                Broken = true;
            }

            _currentIndex++;
        }
    }
}
