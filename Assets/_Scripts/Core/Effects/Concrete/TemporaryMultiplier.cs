using UnityEngine;

namespace Core
{
    public class TemporaryMultiplier : IMultiplierEffect
    {
        public float Factor { get; }
        public bool IsExpired => _remaining <= 0f;

        private float _remaining;

        public TemporaryMultiplier(float factor, float duration)
        {
            Factor = factor;
            _remaining = duration;
        }

        public void Tick(float dt)
        {
            _remaining -= dt;
        }
    }
}
