using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Effects/Permanent Multiplier", fileName = "Permanent Multiplier")]
    public class PermanentMultiplierDefinition : MultiplierEffectDefinition
    {
        public override IMultiplierEffect CreateRuntimeEffect() => new PermanentMultiplier(Factor);
    }
}
