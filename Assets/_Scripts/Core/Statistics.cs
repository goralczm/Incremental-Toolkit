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
        }

        public void SetTotalEarned(float amount)
        {
           _totalEarned = amount;
        }

        public float GetTotalEarned() => _totalEarned;
    }
}
