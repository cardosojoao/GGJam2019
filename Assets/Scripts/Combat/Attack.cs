using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Attack : MonoBehaviour
{
    
    public AudioClip SFX;

    private Animator weapon;
    private AudioSource audioSource;

    private void Awake()
    {
        weapon = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    public IEnumerator Action()
    {
        gameObject.SetActive(true);
        weapon.Play("Attack");
        audioSource.clip = SFX;
        audioSource.Play();
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }

}
