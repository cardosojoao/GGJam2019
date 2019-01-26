using Assets.Scripts.Combat.Sequences;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SequenceData
{
    public string TargetButton;
    public int AttackType;
    public Sprite KeyboardButtonSprite;
    public Sprite ControllerButtonSprite;
}

public class SequenceManager : MonoBehaviour
{
    public SequenceButton SequenceButtonPrefab;
    public Transform sequenceHostGObj;
    public Animator animatorSequence;

    public AttackManager AttackManager;
    public SequenceData[] SequenceTypeArray;

    private Dictionary<string, SequenceData> p_sequenceTypeDictionary;
    private Dictionary<string, SequenceData> _sequenceTypeDictionary
    {
        get
        {
            if (p_sequenceTypeDictionary == null)
            {
                p_sequenceTypeDictionary = new Dictionary<string, SequenceData>();
                if (SequenceTypeArray != null)
                {
                    foreach (SequenceData data in SequenceTypeArray)
                    {
                        p_sequenceTypeDictionary[data.TargetButton] = data;
                    }
                }
            }

            return p_sequenceTypeDictionary;
        }
    }



    private int[] seedSequence = new int[] { 0, 2, 1, 1, 2, 0 };
    private int[] sequence;

    private bool _sequenceActive { get { return _currentSequence != null; } }
    private bool _clearing = false;


    // sequence container
    private Sequence _currentSequence = null;

    // control user input
    private int inputStep;
    private int InputRequired;
    private bool lastStep;
    private Action<bool> _sequenceCallback;

    public void NextSequence(int length, Action<bool> sequenceCallback)
    {
        if (_sequenceActive)
            ClearSequence();

        _sequenceCallback = sequenceCallback;

        if (SequenceTypeArray == null)
            return;
        _currentSequence = SequenceGenerator(length);
    }



    private void Update()
    {
        if (_sequenceActive)
        {
            var buttonPressed = GetButtonPressed();
            if (buttonPressed != null)
                CheckButtonPressed(buttonPressed);
        }
    }

    private void CheckButtonPressed(string buttonPressed)
    {
        var nextButton = _currentSequence.CurrentButton();
        var lastButton = _currentSequence.InFinalButton();

        //correct
        if (buttonPressed == nextButton)
        {
            var currentButtonType = _sequenceTypeDictionary[nextButton];
            if (AttackManager != null)
                AttackManager.Attack(currentButtonType.AttackType, lastButton);

            _currentSequence.ShowClickButton(true);

            if (lastButton)
                ClearSequence();
        }
        else
        //Wrong
        {
            _currentSequence.ShowClickButton(false);
            //animatorSequence.SetTrigger("fail");
            ClearSequence();
        }
    }

    private string GetButtonPressed()
    {
        List<string> _pressedButtonList = new List<string>();
        foreach (SequenceData sequenceType in SequenceTypeArray)
        {
            var buttonName = sequenceType.TargetButton;
            if (Input.GetButtonDown(buttonName))
            {
                _pressedButtonList.Add(buttonName);
            }
        }


        // only 1 button pressed, accept , otherwise ignore
        if (_pressedButtonList.Count == 1)
            return _pressedButtonList[0];

        return null;
    }



    public void ClearSequence()
    {
        var sequenceToClear = _currentSequence;
        if (_sequenceCallback != null)
        {
            var callback = _sequenceCallback;
            _sequenceCallback = null;
            callback(_currentSequence.Finished);
        }

        if (sequenceToClear == _currentSequence)
        {
            _currentSequence.Clear();
            _currentSequence = null;
        }

    }



    private SequenceButton[] GenerateButtonArray(string[] sequence)
    {
        List<SequenceButton> buttonList = new List<SequenceButton>();
        for (int index = 0; index < sequence.Length; index++)
        {
            string button = sequence[index];
            SequenceButton sequenceButton = Instantiate(SequenceButtonPrefab, sequenceHostGObj);

            sequenceButton.SetSequenceData(_sequenceTypeDictionary[button]);
            buttonList.Add(sequenceButton);
        }

        return buttonList.ToArray();
    }



    private Sequence SequenceGenerator(int length)
    {
        var sequence = new string[length];
        var seedLenght = seedSequence.Length;
        var typeLength = SequenceTypeArray.Length;
        for (int index = 0; index < length; index++)
        {
            var buttonTypeIndex = UnityEngine.Random.Range(0, typeLength);
            sequence[index] = SequenceTypeArray[buttonTypeIndex].TargetButton;
        }
        var buttonArray = GenerateButtonArray(sequence);
        return new Sequence(sequence, buttonArray);
    }
}
