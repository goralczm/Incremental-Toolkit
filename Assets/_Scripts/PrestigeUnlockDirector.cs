using UnityEngine;

public class PrestigeUnlockDirector : MonoBehaviour
{
    [SerializeField] private int _threshold = 1;
    [SerializeField] private GameObject _prestigeButton;

    private void Awake()
    {
        Bank.OnCurrencyChanged += (sender, args) =>
        {
            if (((Bank)sender).GetPrestigePoints() >= _threshold)
                _prestigeButton.SetActive(true);
        };
    }
}
