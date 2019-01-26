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
    
    private GameObject[] sequencePrefabs;
    private int[] seedSequence;

    private int seedLenght;
    private int[] sequence;


    private bool sequenceActive;

    public bool[] buttons;


    // sequence container
    private Transform sequenceHostGObj;
    private List<GameObject> sequenceGObjs;

    // control user input
    private int inputStep;
    private int InputRequired;

    private void Awake()
    {
        seedSequence = new int[] { 0, 2, 1, 1, 2, 0 };
        seedLenght = seedSequence.Length;
        sequencePrefabs = new GameObject[] { Sequence1, Sequence2, Sequence3 };
        sequenceHostGObj = transform.Find("Sequence");
    }


    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        sequenceGObjs = new List<GameObject>();
        InvokeRepeating("NewSequence", 3,3);
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
                    Attack.Attack(button);

                    // valid
                    if (!NextInput())
                    {
                        // we have an attack
                    }
                }
                else
                {
                    // failure, cancel sequence;
                    ClearSequence();
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
        inputStep = -1;
        NextInput();
        DisplaySequence();
        sequenceActive = true;
    }


    /// <summary>
    /// Get next required input
    /// </summary>
    /// <returns>if available return true</returns>
    private bool NextInput()
    {
        inputStep++;
        if (inputStep > sequence.Length)
        {
            return false;
        }
        InputRequired = sequence[inputStep];
        return true;
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


}
