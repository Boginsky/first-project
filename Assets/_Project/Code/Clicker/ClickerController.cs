using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class ClickerController : MonoBehaviour 
    {
        [SerializeField] private TMP_Text _textScore;
        [SerializeField] private Button _buttonIncreaseScore;
        [SerializeField] private Button _buttonResetScore;
        [SerializeField] private int _score = 30;

        private int _scoreValue;

        public event Action<int> ScoreChanged;

        private void Awake()
        {
            UpdateTextScore();
            _buttonIncreaseScore.onClick.AddListener(UpdateScore);
            _buttonResetScore.onClick.AddListener(ResetScore);
        }

        private void OnDestroy()
        {
            _buttonIncreaseScore.onClick.RemoveListener(UpdateScore);
            _buttonResetScore.onClick.RemoveListener(ResetScore);
        }
    
        private void UpdateScore()
        {
            _scoreValue += _score;
            UpdateTextScore();
            ScoreChanged?.Invoke(_scoreValue);
        }

        private void ResetScore()
        {
            _scoreValue = 0;
            UpdateTextScore();
            ScoreChanged?.Invoke(_scoreValue);
        }

        private void UpdateTextScore()
        {
            _textScore.SetText($"${_scoreValue.ToString()}");
        }
    }
}