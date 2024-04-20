using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Systems
{
    public sealed class PrefsManager : MonoBehaviour
    {
        private const string GLOBAL_SCORE = "globalScore";
        
        [SerializeField] private ScoreUI _scoreUI;

        private void Start() => _scoreUI.OnScoreChanged += SaveScore;

        private void SaveScore(int globalScore) => PlayerPrefs.SetInt(GLOBAL_SCORE, globalScore);

        private void OnDestroy() => PlayerPrefs.Save();
    }
}
