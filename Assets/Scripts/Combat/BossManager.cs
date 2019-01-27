﻿using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.Util;
using UnityEngine;

public class BossManager : SingletonMonoBehaviour<BossManager>
{
    public float InitalPower;
    public float MaxPower;
    public float CurrentPower;
    public float RecoveryRate = 10f;

    public bool Killed;
    public bool Win;

    public PowerBar powerBar;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        CurrentPower = InitalPower;
        powerBar = GameObject.Find("PowerBar").GetComponent<PowerBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        powerBar.SetPower(CurrentPower, MaxPower);
        animator.Play("Idle");
        InvokeRepeating("IncreasePower", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBoss(DecorationType bossType)
    {
        Debug.Log("Activate " + bossType);
    }

    public void DealDamage(float damage)
    {
        Debug.Log("Boss took " + damage + ".");
        CurrentPower -= damage;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, MaxPower);
        powerBar.SetPower(CurrentPower, MaxPower);

        if (CurrentPower == 0)
        {
            animator.SetTrigger("die");
            Dead();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //DealDamage(0.1f);
    }


    private void IncreasePower()
    {
        CurrentPower += RecoveryRate;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, MaxPower);
        powerBar.SetPower(CurrentPower, MaxPower);

        if (CurrentPower == MaxPower)
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
