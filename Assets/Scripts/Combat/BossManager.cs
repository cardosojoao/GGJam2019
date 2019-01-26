using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.Util;
using UnityEngine;

public class BossManager : SingletonMonoBehaviour<BossManager>
{
    public PowerBar powerBar;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBoss(DecorationType bossType)
    {
        Debug.Log("Activate " + bossType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("hit");
        //powerBar.Hit();
    }
}
