using Assets.Scripts.Util;
using System.Collections;
using UnityEngine;

public class BossManager : SingletonMonoBehaviour<BossManager>
{
    public float InitalPower;
    public float MaxPower;
    public float CurrentPower;
    public float RecoveryRate = 10f;

    public bool Killed;
    public bool Win;
    public bool Animating;

    public PowerBar powerBar;
    public AudioSource HealAudio;

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


    public void DealDamage(float damage)
    {
        CurrentPower -= damage;
        CurrentPower = Mathf.Clamp(CurrentPower, 0, MaxPower);
        powerBar.SetPower(CurrentPower, MaxPower);

        if (CurrentPower == 0)
        {
            animator.SetTrigger("die");
            Animating = true;
            Dead();
        }
        else
        {
            animator.SetTrigger("hit");
            Animating = true;
        }
    }

    public void AnimationFinish()
    {
        Animating = false;
    }

    public IEnumerator WaitForAnimating()
    {
        while (Animating)
            yield return null;
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
        HealAudio.Play();

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
