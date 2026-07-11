using System.Collections;
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
        private int _currentColorIndex;
        private Color _targetColor;
        private Coroutine _transition;

        private void Awake()
        {
            _startColorIndex = Random.Range(0, _colorItems.Length);
            _currentColorIndex = _startColorIndex;
            _imageBG.color = _colorItems[_startColorIndex].Color;
            _targetColor = _imageBG.color;

            _clickerController.ScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            _clickerController.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            int steps = score / _scorePerColor;
            int index = (_startColorIndex + steps) % _colorItems.Length;

            if (index == _currentColorIndex)
            {
                return;
            }

            _currentColorIndex = index;
            _targetColor = _colorItems[index].Color;

            if (_transition != null)
            {
                StopCoroutine(_transition);
            }
            
            _transition = StartCoroutine(SmoothColorTransition());
        }

        private IEnumerator SmoothColorTransition()
        {
            Color start = _imageBG.color;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * _colorChangedSpeed;
                _imageBG.color = Color.Lerp(start, _targetColor, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }

            _imageBG.color = _targetColor;
            _transition = null;
        }
    }
}
