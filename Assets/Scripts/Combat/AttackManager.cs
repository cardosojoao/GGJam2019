using UnityEngine;



public class AttackManager : MonoBehaviour
{
    public GameObject Weapon1Prefab;
    public GameObject Weapon2Prefab;
    public GameObject Weapon3Prefab;

    public GameObject Sequence1;
    public GameObject Sequence2;
    public GameObject Sequence3;

    Attack attack1;
    Attack attack2;
    Attack attack3;

    private int[] seedSequence;
    private int seedLenght;
    private int[] sequence;

    private void Awake()
    {
        seedSequence = new int[] { 0, 2, 1, 1, 2, 0 };
        seedLenght = seedSequence.Length;
    }

    private void Start()
    {
        GameObject weapon1 = GameObject.Instantiate(Weapon1Prefab, transform);
        attack1 = weapon1.GetComponent<Attack>();

        GameObject weapon2 = GameObject.Instantiate(Weapon2Prefab, transform);
        attack2 = weapon2.GetComponent<Attack>();

        GameObject weapon3 = GameObject.Instantiate(Weapon3Prefab, transform);
        attack3 = weapon3.GetComponent<Attack>();

    }

    public void Attack(int attack, bool combo)
    {
        switch (attack)
        {
            case 0:
                {
                    Attack1(combo);
                }
                break;
            case 1:
                {
                    Attack2(combo);
                }
                break;
            case 2:
                {
                    Attack3(combo);
                }
                break;
            default:
                break;
        }
    }

    public void Attack1(bool combo)
    {
        attack1.Action(combo);
    }

    public void Attack2(bool combo)
    {
        attack2.Action(combo);
    }

    public void Attack3(bool combo)
    {
        attack3.Action(combo);
    }

}
