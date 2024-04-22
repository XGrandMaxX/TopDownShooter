using Game.Scripts.Player;
using Game.Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public sealed class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Image _gameOverPanel;
        [SerializeField] private Image _healthImage;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            PlayerCollision.OnTakenDamage += DisplayHealth;
            PlayerCollision.OnDied += EnableGameOverPanel;
            
            _restartButton.onClick.AddListener(GoToMenu);
        }

        private void OnDestroy()
        {
            PlayerCollision.OnTakenDamage -= DisplayHealth;
            PlayerCollision.OnDied -= EnableGameOverPanel;
            
            _restartButton.onClick.RemoveListener(GoToMenu);
        }

        private void DisplayHealth(byte health) 
            => _healthImage.fillAmount = (float)health / PlayerCollision.MaxHealth;

        private void EnableGameOverPanel() => _gameOverPanel.gameObject.SetActive(true);

        private void GoToMenu() => SceneManager.LoadScene(0);
    }
}
