using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public float MaxPower;
    public float RecoveryTime;
    public float HitDamage;

    private Image bar;
    private float Power;

    private void Awake()
    {
        bar = GetComponent<Image>();
        Power = MaxPower;
    }


    public void Hit()
    {
        Power -= HitDamage;
        float strengh = Power / MaxPower;
        bar.fillAmount = strengh;
    }
}
