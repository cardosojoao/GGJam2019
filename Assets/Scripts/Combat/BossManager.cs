using Assets.Scripts.Util;
using UnityEngine;

public enum BossType
{
    Painting,

}

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

    public void SetBoss(BossType bossType)
    {
        Debug.Log("Activate " + bossType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("hit");
        //powerBar.Hit();
    }
}
