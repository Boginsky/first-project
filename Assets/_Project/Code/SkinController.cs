using Project.Game;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Code
{
    public class SkinController : MonoBehaviour
    {
        [SerializeField] private Image _imageBG;
        [SerializeField] private ColorItem[] _colorItems;
        [SerializeField] private ClickerController _clickerController;

        [SerializeField] private int _scorePerColor = 1000;
        [SerializeField] private float _colorChangedSpeed = 2f;
        private int _startColorIndex;
        private Color _targetColor;
        
        private void Awake()
        {
            _startColorIndex = Random.Range(0, _colorItems.Length);
            _imageBG.color = _colorItems[_startColorIndex].Color;
            _targetColor = _imageBG.color;
            
            _clickerController.OnScoreChanged += OnScoreChanged;
        }

        private void Update()
        {
            _imageBG.color = Color.Lerp(_imageBG.color, _targetColor, Time.deltaTime * _colorChangedSpeed);
        }

        private void OnDestroy()
        {
            _clickerController.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            int steps = score / _scorePerColor;
            int index = (_startColorIndex + steps) % _colorItems.Length;
            _targetColor = _colorItems[index].Color;
        }
    }
}