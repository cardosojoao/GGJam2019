using Assets.Scripts.Attic.Interaction;
using Assets.Scripts.Memory;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Attic.Decorations
{
    public class DecorationInteract : InteractionObject
    {
        public DecorationObject DecorationObject;
        public string ButtonKey = "Activate";
        public string CombatScene = "Combat";
        public string MemoryScene = "Memory";
        public Fader Fader;
        public AudioSource ClickSound;

        public override void ActivateObject()
        {
            if (DecorationObject.State == DecorationState.Good || DecorationObject.State == DecorationState.TurningGood)
                return;

            Fader.FadeIn();
        }

        public override void DeactivateObject()
        {
            Fader.FadeOut();
        }

        private void Update()
        {
            if (Input.GetButtonDown(ButtonKey) && !GameManager.Instance.Paused)
            {
                ToNextScene();
            }
        }

        private void ToNextScene()
        {
            ClickSound.Play();
            if (DecorationObject.CombatSkip)
                GameManager.Instance.OpenScene(MemoryScene, SetMemory);
            else
                StartCombat();
        }

        private void StartCombat()
        {
            GameManager.Instance.OpenScene(CombatScene, SetBoss);
        }

        private IEnumerator SetBoss()
        {
            while (CombatManager.Instance == null)
                yield return null;
            CombatManager.Instance.SetBoss(DecorationObject.DecorationType);
        }

        private IEnumerator SetMemory()
        {
            while (MemoryManager.Instance == null)
                yield return null;
            MemoryManager.Instance.SetMemory(DecorationObject.DecorationType);
        }
    }
}
