﻿using Assets.Scripts.Util;
using UnityEngine;

public enum BossType
{
    Painting,

}

public class BossManager : SingletonMonoBehaviour<BossManager>
{
    public float InitalPower;
    public float MaxPower;
    public float CurrentPower;

    public bool Killed;
    public bool Win;

    public PowerBar powerBar;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        CurrentPower = InitalPower;
    }

    // Start is called before the first frame update
    void Start()
    {
        powerBar.SetPower(CurrentPower);
        animator.Play("Idle");
        InvokeRepeating("IncreasePower", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBoss(BossType bossType)
    {
        Debug.Log("Activate " + bossType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentPower -= .1f;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, 1);
        powerBar.SetPower(CurrentPower);

        if( CurrentPower == 0)
        {
            animator.SetTrigger("die");
            Dead();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }


    private void IncreasePower()
    {
        CurrentPower += .1f;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, 1);
        powerBar.SetPower(CurrentPower);

        if( CurrentPower == MaxPower)
        {
            Won();
        }
    }

    private void Dead()
    {
        Killed = true;
        CancelInvoke("IncreasePower");
    }

    private void Won()
    {
        Win = true;
        CancelInvoke("IncreasePower");
    }
}
