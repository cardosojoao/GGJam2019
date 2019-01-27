using Assets.Scripts.Combat.Sequences;
using System;
using System.Collections;
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
    public AudioClip GoodSequence;
    public AudioClip BadSequence;
    public RefreshBar RefreshBar;

    public AttackManager AttackManager;
    public SequenceData[] SequenceTypeArray;
    public AudioSource AudioSourceSFX;

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


    // sequence container
    private Sequence _currentSequence = null;
    private bool _clearing = false;

    // control user input
    private int inputStep;
    private int InputRequired;
    private bool lastStep;
    private Action<bool> _sequenceCallback;
    private bool _animating;

    public IEnumerator NextSequence(int length, Action<bool> sequenceCallback)
    {
        if (_sequenceActive)
            yield return ClearSequenceRoutine();

        while (_animating)
            yield return null;
        while (_clearing)
            yield return null;

        _sequenceCallback = sequenceCallback;

        if (SequenceTypeArray == null)
            yield break;
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
        if (_currentSequence.Broken)
        {
            Animate("fail");
            PlayClip(BadSequence);
            return;
        }

        if (_currentSequence.Finished)
            return;
        var nextButton = _currentSequence.CurrentButton();
        var lastButton = _currentSequence.InFinalButton();

        //correct
        if (buttonPressed == nextButton)
        {
            var currentButtonType = _sequenceTypeDictionary[nextButton];

            _currentSequence.ShowClickButton(true);
            if (AttackManager != null)
            {
                var combo = lastButton && _currentSequence.Count > 1;
                AttackManager.Attack(currentButtonType.AttackType, combo);
            }


            if (lastButton)
            {
                ClearSequence();
                PlayClip(GoodSequence);
            }
        }
        else
        //Wrong
        {
            _currentSequence.ShowClickButton(false);
            Animate("fail");
            RefreshBar.TriggerError();
            PlayClip(BadSequence);

            CallCallback();
        }
    }

    private void PlayClip(AudioClip clip)
    {
        AudioSourceSFX.Stop();
        AudioSourceSFX.clip = clip;
        AudioSourceSFX.Play();
    }

    private void Animate(string triggerName)
    {
        animatorSequence.SetTrigger(triggerName);
        _animating = true;
    }

    private void AnimationDone()
    {
        _animating = false;
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
        StartCoroutine(ClearSequenceRoutine());
    }

    public IEnumerator ClearSequenceRoutine()
    {
        if (_currentSequence == null)
            yield break;
        _clearing = true;
        var sequenceToClear = _currentSequence;
        var callback = _sequenceCallback;

        yield return _currentSequence.Clear();
        _currentSequence = null;

        if (callback != null)
        {
            _sequenceCallback = null;
            callback(sequenceToClear.Finished);
        }
        _clearing = false;

    }

    private void CallCallback()
    {
        var callback = _sequenceCallback;
        if (callback != null)
        {
            _sequenceCallback = null;
            callback(_currentSequence.Finished);
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
