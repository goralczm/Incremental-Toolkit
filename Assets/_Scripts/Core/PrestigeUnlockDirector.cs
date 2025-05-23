using UnityEngine;

namespace Core
{
    public class PrestigeUnlockDirector : MonoBehaviour
    {
        [SerializeField] private int _threshold = 1;
        [SerializeField] private GameObject _prestigeButton;

        private void Awake()
        {
            Bank.OnCurrencyChanged += (sender, args) =>
            {
                _prestigeButton.SetActive(((Bank)sender).CalculatePrestigePoints() >= _threshold);
            };
        }
    }
}