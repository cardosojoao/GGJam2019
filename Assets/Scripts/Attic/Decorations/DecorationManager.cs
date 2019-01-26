using Assets.Scripts.Util;
using System.Collections.Generic;

namespace Assets.Scripts.Attic.Decorations
{
    public class DecorationManager : SingletonMonoBehaviour<DecorationManager>
    {
        public Dictionary<DecorationType, DecorationState> DecorationStateDictionary = new Dictionary<DecorationType, DecorationState>();


        public void SetDecorationState(DecorationType decorationType, DecorationState decorationState)
        {
            DecorationStateDictionary[decorationType] = decorationState;
        }

        public void Clear()
        {
            DecorationStateDictionary.Clear();
        }
    }
}
