using UnityEngine;

namespace Game.Scripts.Systems
{
    public sealed class BackgroundMusic : MonoBehaviour
    {
        private BackgroundMusic Instance;
        
        [SerializeField] internal AudioSource _musicSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}
