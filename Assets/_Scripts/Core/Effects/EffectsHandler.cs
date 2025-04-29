using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class EffectsHandler : MonoBehaviour
    {
        private readonly List<IMultiplierEffect> _effects = new();

        public float GetTotalMultiplier()
        {
            return _effects
                .Select(e => e.Factor)
                .Aggregate(1.0f, (a, f) => a * f);
        }

        public void Tick(float deltaTime)
        {
            foreach (var e in _effects) e.Tick(deltaTime);
            _effects.RemoveAll(e => e.IsExpired);
        }

        public void AddEffect(IMultiplierEffect effect)
        {
            _effects.Add(effect);
        }

        public void ClearEffects()
        {
            _effects.Clear();
        }

        public List<IMultiplierEffect> GetEffects() => _effects;
    }
}
