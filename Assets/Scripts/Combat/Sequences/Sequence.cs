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

        public bool Finished { get { return _currentIndex >= _currentSequence.Length; } }

        public void Clear()
        {
            for (int index = 0; index < _buttonArray.Length; index++)
            {
                _buttonArray[index].SelfDestroy();
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
                _buttonArray[_currentIndex].ClickWrong();

            _currentIndex++;
        }
    }
}
