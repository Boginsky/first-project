using Project.Code;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class ClickerController : MonoBehaviour
    {
        [SerializeField] private Observer _observer;
        
        [SerializeField] private TMP_Text _textScore;
        [SerializeField] private Button _buttonIncreaseScore;
        [SerializeField] private Button _buttonResetScore;
        [SerializeField] private Button _buttonUpgrade;
        [SerializeField] private int _upgradeIncrement = 100;
        
        private BankService _bankService;

        private void Awake()
        {
            _bankService = _observer.GetBank();
            UpdateTextScore();
            _buttonIncreaseScore.onClick.AddListener(UpdateScore);
            _buttonResetScore.onClick.AddListener(ResetScore);
            _buttonUpgrade.onClick.AddListener(OnUpgradeClicked);
            _buttonUpgrade.interactable = !_bankService.IsClickUpgradeMaxed();
        }

        private void OnDestroy()
        {
            _buttonIncreaseScore.onClick.RemoveListener(UpdateScore);
            _buttonResetScore.onClick.RemoveListener(ResetScore);
            _buttonUpgrade.onClick.RemoveListener(OnUpgradeClicked);
        }
    
        private void UpdateScore()
        {
            _bankService.UpdateScore();
            UpdateTextScore();
        }

        private void ResetScore()
        {
            _bankService.SetScore(0);
            UpdateTextScore();
        }

        private void UpdateTextScore()
        {
            _textScore.SetText($"${_bankService.GetScore().ToString()}");
        }
        
        private void OnUpgradeClicked()
        {
            _bankService.UpdateScorePerClick(_upgradeIncrement);
            _buttonUpgrade.interactable = !_bankService.IsClickUpgradeMaxed();
        }
    }
}