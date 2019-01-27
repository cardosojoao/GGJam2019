using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Sequences
{
    public class Sequence
    {
        private string[] _currentSequence;
        private int _currentIndex;
        private SequenceButton[] _buttonArray;

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


            foreach (SequenceButton button in _buttonArray)
            {
                button.SelfDestroy();
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
                _buttonArray[_currentIndex].ClickWrong();
                Broken = true;
            }

            _currentIndex++;
        }
    }
}
