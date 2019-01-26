using Assets.Scripts.Util;
using System.Collections.Generic;

namespace Assets.Scripts.Attic.Decorations
{
    public class DecorationManager : SingletonMonoBehaviour<DecorationManager>
    {
        public Dictionary<DecorationType, DecorationState> DecorationState = new Dictionary<DecorationType, DecorationState>();


        public void Clear()
        {
            DecorationState.Clear();
        }
    }
}
