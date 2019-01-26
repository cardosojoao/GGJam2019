using Assets.Scripts.Attic.Decorations;
using Assets.Scripts.Util;

namespace Assets.Scripts.Memory
{
    public class MemoryManager : SingletonMonoBehaviour<MemoryManager>
    {
        public MemoryReel MemoryReel;
        public void SetMemory(DecorationType decorationType)
        {
            MemoryReel.CurrnetMemoryReel = decorationType;
        }
    }
}
