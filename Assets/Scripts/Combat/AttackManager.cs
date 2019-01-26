using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Attack Weapon1;
    public Attack Weapon2;
    public Attack Weapon3;



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
}
