using System;
using Game.Scripts.Enemies;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public sealed class ScoreUI : MonoBehaviour
    {
        public event Action<int> OnScoreChanged;
        
        private int globalScore;
        
        [SerializeField] private TMP_Text _scoreText;
        private EnemyListener _enemyListener;

        [Inject]
        private void Construct(EnemyListener enemyListener)
        {
            _enemyListener = enemyListener;
            _enemyListener.OnEnemyDied += UpdateScore;
        }

        private void UpdateScore(int amount)
        {
            _scoreText.text = $"{globalScore += amount}";
            
            OnScoreChanged?.Invoke(globalScore);
        }
    }
}
