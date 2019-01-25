using UnityEngine;

namespace Assets.Scripts.Util
{
    public class PersistentMonoBehaviour <T> : MonoBehaviour
        where T : PersistentMonoBehaviour<T>
    {
        public T Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}
