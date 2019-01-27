using UnityEngine;



public class Attack : MonoBehaviour
{

    public AudioClip SFX;
    public float damage;

    private float attackDamage;
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
            attackDamage = damage * CombatManager.Instance.ComboDamageMultiplier;
        }
        else
        {
            attackDamage = damage;
        }

        gameObject.SetActive(true);
        weapon.Play("Attack");
        audioSource.clip = SFX;
        audioSource.Play();

    }

    public void DealDamage()
    {
        CombatManager.Instance.CurrentBoss.DealDamage(attackDamage);
    }

    public void FinishAttack()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
