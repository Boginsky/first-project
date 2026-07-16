using UnityEngine;

namespace Project.Code
{
    [DefaultExecutionOrder(-100)]
    public class Observer : MonoBehaviour
    {
        private BankService _bankService;
        private SkinService _skinService;

        private void Awake()
        {
            _bankService = new BankService(0);
            _skinService = new SkinService();
        }

        public BankService GetBank()
        {
            return _bankService;
        }

        public SkinService GetSkin()
        {
            return _skinService;
        }
    }
}