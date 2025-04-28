using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Effects/Temporary Multiplier", fileName = "Temporary Multiplier")]
    public class TemporaryMultiplierDefinition : MultiplierEffectDefinition
    {
        [Tooltip("Effect duration in seconds")]
        public float Duration;

        public override IMultiplierEffect CreateRuntimeEffect() => new TemporaryMultiplier(Factor, Duration);
    }
}
