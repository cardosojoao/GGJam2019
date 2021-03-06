﻿using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    private Image bar;

    private void Awake()
    {
        bar = transform.Find("Power").GetComponentInChildren<Image>();
    }

    public void SetPower(float power, float maxPower)
    {
        bar.fillAmount = power / maxPower;
    }
}
