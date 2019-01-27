using Assets.Scripts.Util;
using System.Collections.Generic;

namespace Assets.Scripts.Attic.Decorations
{
    public class DecorationManager : SingletonMonoBehaviour<DecorationManager>
    {
        private static DecorationType[] INTERACTION_ORDER = new DecorationType[]
        {
            DecorationType.Painting,
            DecorationType.Belt,
            DecorationType.FireIron,
            DecorationType.Chair
        };

        public Dictionary<DecorationType, DecorationState> DecorationStateDictionary = new Dictionary<DecorationType, DecorationState>();


        public DecorationState GetDecorationState(DecorationType type)
        {
            if (!DecorationStateDictionary.ContainsKey(type))
                return DecorationState.Evil;

            return DecorationStateDictionary[type];
        }

        public DecorationType NextDecoration()
        {
            foreach (DecorationType decoration in INTERACTION_ORDER)
            {
                if (!DecorationStateDictionary.ContainsKey(decoration))
                    return decoration;

                DecorationState decorationState = DecorationStateDictionary[decoration];
                if (decorationState == DecorationState.Evil)
                    return decoration;
            }

            return DecorationType.Chair;
        }

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
