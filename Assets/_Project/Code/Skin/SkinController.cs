using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Project.Code
{
    public class SkinController : MonoBehaviour
    {
        [SerializeField] private Observer _observer;
        
        [SerializeField] private Image _imageBG;
        [SerializeField] private ColorItem[] _colorItems;
        [SerializeField] private int _scorePerColor = 1000;
        [SerializeField] private float _colorChangedSpeed = 2f;
        
        private BankService _bankService;
        private SkinService _skinService;
        private Coroutine _transition;

        private void Awake()
        {
            _bankService = _observer.GetBank();
            _skinService = _observer.GetSkin();
            _skinService.Initialize(_colorItems, _scorePerColor);
            
            _imageBG.color = _skinService.GetTargetColor();
            _bankService.ScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            _bankService.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            if (!_skinService.OnScoreChanged(score))
            {
                return;                              
            }

            if (_transition != null)
            {
                StopCoroutine(_transition);
            }

            _transition = StartCoroutine(SmoothColorTransition());
        }
        
        private IEnumerator SmoothColorTransition()
        {
            var start = _imageBG.color;
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime * _colorChangedSpeed;
                _imageBG.color = Color.Lerp(start, _skinService.GetTargetColor(), Mathf.SmoothStep(0f, 1f, time));
                yield return null;
            }

            _imageBG.color = _skinService.GetTargetColor();
        }
    }
}
