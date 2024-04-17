using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [Min(0), SerializeField] private byte _loadSceneIndex = 1;
        
        private void Start() => _playButton.onClick.AddListener(LoadScene);
        private void LoadScene() => SceneManager.LoadScene(_loadSceneIndex);
    }
}
