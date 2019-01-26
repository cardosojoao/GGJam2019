using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public float CombateFrequency;
    public bool Active;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetAttack",2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CombateFinished()
    {
        Active = false;
    }


    private void SetAttack()
    {

    }

}
