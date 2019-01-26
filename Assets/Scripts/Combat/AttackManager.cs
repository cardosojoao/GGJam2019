using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Attack Weapon1;
    public Attack Weapon2;
    public Attack Weapon3;

    public GameObject Sequence1;
    public GameObject Sequence2;
    public GameObject Sequence3;



    private int[] seedSequence;
    private int seedLenght;
    private int[] sequence;

    private void Awake()
    {
        seedSequence = new int[] { 0, 2, 1, 1, 2, 0 };
        seedLenght = seedSequence.Length;
    }


    public void Attack(int attack)
    {
        switch (attack)
        {
            case 0:
                {
                    Attack1();
                }
                break;
            case 1:
                {
                    Attack2();
                }
                break;
            case 2:
                {
                    Attack3();
                }
                break;
            default:
                break;
        }
    }

    public void Attack1()
    {
        StartCoroutine(Weapon1.Action());
    }
    
    public void Attack2()
    {
        StartCoroutine(Weapon2.Action());
    }

    public void Attack3()
    {
        StartCoroutine(Weapon3.Action());
    }

    private void SequenceGenerator(int lenght)
    {
        sequence = new int[lenght];
        for (int index = 0; index < lenght; index++)
        {
            sequence[index] = seedSequence[Random.Range(0, seedLenght - 1)];
        }
    }
}
