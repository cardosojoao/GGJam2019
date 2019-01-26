using UnityEngine;

namespace Assets.Scripts.Attic.Interaction
{
    public abstract class InteractionObject : MonoBehaviour
    {
        public abstract void ActivateObject();
        public abstract void DeactivateObject();
    }
}
