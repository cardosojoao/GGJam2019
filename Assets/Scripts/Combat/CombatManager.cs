using Assets.Scripts;
using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.Combat.Sequences;
using Assets.Scripts.Memory;
using Assets.Scripts.Util;
using System.Collections;
using UnityEngine;

public class CombatManager : SingletonMonoBehaviour<CombatManager>
{
    public BossManager PaintingBoss;
    public BossManager FireIronBoss;
    public BossManager ChairBoss;
    public BossManager BeltBoss;
    public SequenceWaveManager sequenceManager;
    public GameObject bossContainer;

    public float CombateFrequency;
    public bool Active;
    public bool Debug;
    public DecorationType DebugBoss;
    public float ComboDamageMultiplier;
    public bool Started { get; private set; }

    [HideInInspector]
    public BossManager CurrentBoss;
    private DecorationType boss;

    public void SetBoss(DecorationType bossType)
    {
        boss = bossType;
        BossManager bossPrefab;
        switch (bossType)
        {
            case DecorationType.FireIron:
                bossPrefab = FireIronBoss;
                break;
            case DecorationType.Chair:
                bossPrefab = ChairBoss;
                break;
            case DecorationType.Belt:
                bossPrefab = BeltBoss;
                break;
            case DecorationType.Painting:
            default:
                bossPrefab = PaintingBoss;
                break;
        }
        CurrentBoss = Instantiate(bossPrefab, bossContainer.transform);

        Active = true;
        Started = true;
        sequenceManager.StartWaves(0);
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Debug && !Started)
        {
            SetBoss(DebugBoss);
        }
#endif

        if (Active)
        {
            if (CurrentBoss.Killed)
            {
                CombateFinished();
                StartMemory();
            }
            else if (CurrentBoss.Win)
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
