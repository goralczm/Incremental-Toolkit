using UnityEngine;

namespace Core
{
    public class Statistics : MonoBehaviour
    {
        private float _totalEarned;

        private void Awake()
        {
            Bank.OnCurrencyEarned += (amount) =>
            {
                SetTotalEarned(GetTotalEarned() + amount);
            };

            Bank.OnPrestige += (sender, args) =>
            {
                SetTotalEarned(0);
            };
        }

        public void SetTotalEarned(float amount)
        {
           _totalEarned = amount;
        }

        public float GetTotalEarned() => _totalEarned;
    }
}
