using Game.Scripts.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public sealed class SettingsUI : MonoBehaviour
    {
        private InputActions _inputActions;
        [SerializeField] private Image _settingsPanel;
        [SerializeField] private Button _settingsButton;
        
        public void SetQuality(int qualityIndex)
            => QualitySettings.SetQualityLevel(qualityIndex);

        private void OnEnable() 
            => _settingsButton.onClick.AddListener(DisplaySettings);

        private void DisplaySettings()
        {
            _settingsPanel.gameObject.SetActive(!_settingsPanel.gameObject.activeInHierarchy);
            TimeScaleController.Pause2UnpauseGame();
        }

        private void OnDestroy() 
            => _settingsButton.onClick.RemoveListener(DisplaySettings);
    }
}
