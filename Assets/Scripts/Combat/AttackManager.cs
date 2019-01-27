using UnityEngine;



public class AttackManager : MonoBehaviour
{
    public Attack attack1;
    public Attack attack2;
    public Attack attack3;
    public Transform attackContainer;

    public void Attack(int attack, bool combo)
    {
        switch (attack)
        {
            case 0:
                {
                    InstantiateAttack(attack1, combo);
                }
                break;
            case 1:
                {
                    InstantiateAttack(attack2, combo);
                }
                break;
            case 2:
                {
                    InstantiateAttack(attack3, combo);
                }
                break;
            default:
                break;
        }
    }


    private void InstantiateAttack(Attack attack, bool combo)
    {
        Attack attackObject = Instantiate(attack, attackContainer);
        attackObject.Action(combo);
    }
}
