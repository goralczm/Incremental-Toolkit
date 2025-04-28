using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class EffectsHandler
    {
        private readonly List<IMultiplierEffect> _effects = new();

        public float GetTotalMultiplier()
        {
            return _effects
                .Select(e => e.Factor)
                .Aggregate(1.0f, (a, f) => a * f);
        }

        public void Update(float deltaTime)
        {
            foreach (var e in _effects) e.Tick(deltaTime);
            _effects.RemoveAll(e => e.IsExpired);
        }

        public void AddEffect(IMultiplierEffect effect)
        {
            _effects.Add(effect);
        }
    }
}
