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


    public void Attack(int attack, bool combo)
    {
        switch (attack)
        {
            case 0:
                {
                    Attack1(combo);
                }
                break;
            case 1:
                {
                    Attack2(combo);
                }
                break;
            case 2:
                {
                    Attack3(combo);
                }
                break;
            default:
                break;
        }
    }

    public void Attack1(bool combo)
    {
        Weapon1.Action(combo);
    }
    
    public void Attack2(bool combo)
    {
        Weapon2.Action(combo);
    }

    public void Attack3(bool combo)
    {
        Weapon3.Action(combo);
    }

}
