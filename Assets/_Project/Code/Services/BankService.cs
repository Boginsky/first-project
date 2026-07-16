using System;

namespace Project.Code
{
    public class BankService
    { 
        private const int MaxScorePerClick = 3000;

        private int _scoreValue;
        private int _scorePerClick = 100;
        
        public event Action<int> ScoreChanged;
        
        public BankService(int scoreValue)
        {
            _scoreValue = Math.Max(0, scoreValue);        
        }
        
        public int GetScore()
        {
            return _scoreValue;
        }

        public void SetScore(int score)
        {
            _scoreValue = Math.Max(0, score);
            ScoreChanged?.Invoke(_scoreValue);
        }
        
        public void UpdateScore()
        {
            _scoreValue += _scorePerClick;
            ScoreChanged?.Invoke(_scoreValue);
        }
        
        public void UpdateScorePerClick(int increment)
        {
            if (increment <= 0)
            {
                return;
            }

            if (_scorePerClick >= MaxScorePerClick) 
            {
                return;
            }

            _scorePerClick = Math.Min(_scorePerClick + increment, MaxScorePerClick);
        }

        public bool IsClickUpgradeMaxed()
        {
            return _scorePerClick >= MaxScorePerClick;
        }
    }
}