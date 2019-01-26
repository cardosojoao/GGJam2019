using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Attack : MonoBehaviour
{
    
    public AudioClip SFX;
    public float damage;
    public float attackDamage;

    private Animator weapon;
    private AudioSource audioSource;

    private void Awake()
    {
        weapon = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void Action(bool combo)
    {
        if (combo)
        {
            attackDamage *= 3;
        }
        else
        {
            attackDamage = damage;
        }

        gameObject.SetActive(true);
        weapon.Play("Attack");
        audioSource.clip = SFX;
        audioSource.Play();
        //yield return new WaitForSeconds(.5f);
        
    }

    public void FinishAttack()
    {
        gameObject.SetActive(false);
    }

}
