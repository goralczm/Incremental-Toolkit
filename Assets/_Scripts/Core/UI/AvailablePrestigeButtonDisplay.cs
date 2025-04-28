using TMPro;
using UnityEngine;

namespace Core
{
    public class AvailablePrestigeButtonDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prestigeButtonText;

        private void Awake()
        {
            Bank.OnAvailablePrestigePointsChanged += (sender, args) =>
            {
                _prestigeButtonText.SetText($"Prestige for {args.AvailablePrestigePoints} PP");
            };
        }
    }
}
