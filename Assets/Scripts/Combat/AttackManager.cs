using UnityEngine;



public class AttackManager : MonoBehaviour
{
    public Attack attack1;
    public Attack attack2;
    public Attack attack3;

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
