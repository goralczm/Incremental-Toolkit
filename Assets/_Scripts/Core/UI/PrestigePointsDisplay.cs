using TMPro;
using UnityEngine;

namespace Core
{
    public class PrestigePointsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prestigePointsText;

        private void Awake()
        {
            Bank.OnPrestigePointsChanged += (sender, args) =>
            {
                gameObject.SetActive(args.PrestigePoints > 0);
                _prestigePointsText.SetText($"PP: {args.PrestigePoints}");
            };
        }
    }
}
