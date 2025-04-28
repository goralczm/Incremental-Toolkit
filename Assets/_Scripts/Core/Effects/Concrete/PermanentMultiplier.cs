using UnityEngine;

namespace Core
{
    public class PermanentMultiplier : IMultiplierEffect
    {
        public float Factor { get; }
        public bool IsExpired => false;
        public PermanentMultiplier(float factor) => Factor = factor;
        public void Tick(float _) { }
    }
}
