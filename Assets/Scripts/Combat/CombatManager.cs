using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.Util;
using Assets.Scripts.Memory;
    using Assets.Scripts;

public class CombatManager : SingletonMonoBehaviour<CombatManager>
{
    public GameObject Boos1;
    public GameObject Boos2;
    public GameObject Boss3;
    public GameObject Boss4;
    public SequenceManager sequenceManager;

    public float CombateFrequency;
    public bool Active;

    private GameObject currentBoss;
    private BossManager bossManager;
    private DecorationType boss;
    private GameObject bossContainer;

    public void SetBoss(DecorationType bossType)
    {
        bossContainer = GameObject.Find("bossContainer");
        boss = bossType;
        switch (bossType)
        {
            case DecorationType.Painting:
                break;
            case DecorationType.FireIron:
                break;
            case DecorationType.Chair:
                {
                    currentBoss = GameObject.Instantiate(Boss3, bossContainer.transform);
                }
                break;
            case DecorationType.Belt:
                break;
            default:
                break;
        }
        bossManager = currentBoss.GetComponent<BossManager>();

        Active = true;
        sequenceManager.StartSequences();
    }


    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (bossManager.Killed)
            {
                CombateFinished();
                StartMemory();
            }
            else if (bossManager.Win)
            {
                CombateFinished();
                StartAttik();
            }
        }
    }


    public void CombateFinished()
    {
        Active = false;
    }


    private void StartMemory()
    {
        //ClickSound.Play();
        GameManager.Instance.OpenScene("Memory", SetMemory);
    }

    private IEnumerator SetMemory()
    {
        while (MemoryManager.Instance == null)
            yield return null;
        MemoryManager.Instance.SetMemory(boss);

    }


    private void StartAttik()
    {
        //ClickSound.Play();
        GameManager.Instance.OpenScene("attic");
    }
}
