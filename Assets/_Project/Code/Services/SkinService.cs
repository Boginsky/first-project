using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code
{
    public class SkinService
    {
        private ColorItem[] _items;
        private int _scorePerColor;
        
        private int _startColorIndex;
        private int _currentColorIndex;
        private Color _targetColor;

        public void Initialize(ColorItem[] items, int scorePreColor)
        {
            _items = items;
            _scorePerColor = scorePreColor;
            
            _startColorIndex = Random.Range(0, items.Length);
            _currentColorIndex = _startColorIndex;

            var currentColor = items[_startColorIndex].Color;
            _targetColor = currentColor;
        }
        
        public bool OnScoreChanged(int score)
        {
            int steps = score / _scorePerColor;
            int index = (_startColorIndex + steps) % _items.Length;

            if (index == _currentColorIndex)
            {
                return false;                       
            }

            _currentColorIndex = index;
            _targetColor = _items[index].Color;
            return true;                          
        }
        
        public Color GetTargetColor()
        {
            return _targetColor;
        }
    }
}