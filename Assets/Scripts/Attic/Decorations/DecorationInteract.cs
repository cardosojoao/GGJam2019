using Assets.Scripts.Attic.Interaction;
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
        public Fader Fader;
        public AudioSource ClickSound;

        public override void ActivateObject()
        {
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
                StartCombat();
            }
        }

        private void StartCombat()
        {
            ClickSound.Play();
            GameManager.Instance.OpenScene(CombatScene, SetBoss);
        }

        private IEnumerator SetBoss()
        {
            while (BossManager.Instance == null)
                yield return null;
            BossManager.Instance.SetBoss(DecorationObject.DecorationType);

        }
    }
}
