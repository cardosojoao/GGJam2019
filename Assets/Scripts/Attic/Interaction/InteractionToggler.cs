using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.Attic.Interaction
{
    public class InteractionToggler : MonoBehaviour
    {
        public InteractionObject TargetInteraction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == CharacterPerspController.CHARACTER_TAG)
            {
                TargetInteraction.ActivateObject();
            }

        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == CharacterPerspController.CHARACTER_TAG)
            {
                TargetInteraction.DeactivateObject();
            }
        }
    }
}
