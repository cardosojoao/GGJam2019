﻿/** /
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public GameObject Sequence1;
    public GameObject Sequence2;
    public GameObject Sequence3;

    public AttackManager Attack;
    public int SequenceLenght;

    public AudioClip GoodSequence;
    public AudioClip BadSequence;

    private GameObject[] sequencePrefabs;
    private int[] seedSequence;

    private int seedLenght;
    private int[] sequence;


    private bool sequenceActive;

    public bool[] buttons;


    // sequence container
    private Transform sequenceHostGObj;
    private Animator animatorSequence;
    private List<GameObject> sequenceGObjs;

    private AudioSource audioSourceFX;


    // control user input
    private int inputStep;
    private int InputRequired;
    private bool lastStep;

    private void Awake()
    {
        seedSequence = new int[] { 0, 2, 1, 1, 2, 0 };
        seedLenght = seedSequence.Length;
        sequencePrefabs = new GameObject[] { Sequence1, Sequence2, Sequence3 };
        sequenceHostGObj = transform;
        animatorSequence = sequenceHostGObj.GetComponent<Animator>();
        audioSourceFX = GetComponent<AudioSource>();
    }


    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
    }

    public void StartSequences()
    {
        sequenceGObjs = new List<GameObject>();
        InvokeRepeating("NewSequence", 3, 3);
    }

    private void StopSequences()
    {
        sequenceActive = false;
        CancelInvoke("NewSequence");
        ClearSequence();
    }

    private void Update()
    {
        if (sequenceActive)
        {
            // get all the buttons
            buttons = new bool[] { Input.GetButtonDown("Button1"), Input.GetButtonDown("Button2"), Input.GetButtonDown("Button3") };

            // check how  many buttons go pressed
            int count = 0;
            int button = -1;
            for (int index = 0; index < buttons.Length; index++)
            {
                if (buttons[index])
                {
                    button = index;
                    count++;
                }
            }

            // only 1 button pressed, accept , otherwise ignore
            if (count == 1)
            {
                // valid action only one button got pressed
                if (button == InputRequired)
                {
                    Attack.Attack(button, lastStep);

                    if (lastStep)
                    {
                        audioSourceFX.clip = GoodSequence;
                        audioSourceFX.Play();
                        animatorSequence.SetTrigger("combo");
                        //ClearSequence();
                    }
                    else
                    {
                        lastStep = NextInput();
                    }
                }
                else
                {
                    audioSourceFX.clip = BadSequence;
                    audioSourceFX.Play();
                    animatorSequence.SetTrigger("fail");
                        // failure, cancel sequence;
                        //ClearSequence();
                }
            }
        }
    }




    private void ClearSequence()
    {
        sequenceActive = false;
        //SequenceButton[] buttons = sequenceHostGObj.GetComponentsInChildren<SequenceButton>(true);
        for (int index = 0; index < sequenceGObjs.Count; index++)
        {
            GameObject.Destroy(sequenceGObjs[index]);
        }
    }


    /// <summary>
    /// Create new sequence
    /// </summary>
    private void NewSequence()
    {
        ClearSequence();
        SequenceGenerator(3);
        inputStep = 0;
        lastStep = NextInput();
        DisplaySequence();
        sequenceActive = true;
    }


    /// <summary>
    /// Get next required input
    /// </summary>
    /// <returns>if available return true</returns>
    private bool NextInput()
    {
        InputRequired = sequence[inputStep];
        inputStep++;
        return inputStep >= sequence.Length;
    }

    /// <summary>
    /// calculate new sequence
    /// </summary>
    /// <param name="lenght"></param>
    private void SequenceGenerator(int lenght)
    {
        sequence = new int[lenght];
        for (int index = 0; index < lenght; index++)
        {
            sequence[index] = seedSequence[Random.Range(0, seedLenght - 1)];
        }
    }

    /// <summary>
    /// display sequence
    /// </summary>
    private void DisplaySequence()
    {
        Vector3 pos = Vector3.zero;
        sequenceGObjs = new List<GameObject>();
        for (int index = 0; index < sequence.Length; index++)
        {
            int sequenceID = sequence[index];
            GameObject button = GameObject.Instantiate(sequencePrefabs[sequenceID], pos, Quaternion.identity, sequenceHostGObj);
            sequenceGObjs.Add(button);
            pos += new Vector3(1, 0);
        }
    }


    public void FinishFail()
    {
        ClearSequence();
    }

    public void FinishCombo()
    {
        ClearSequence();
    }

}

*/