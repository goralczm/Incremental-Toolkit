using UnityEngine;

namespace Core
{
    public abstract class MultiplierEffectDefinition : ScriptableObject
    {
        [Tooltip("1.0 = no change, 1.5 = +50%, 2.0 = +100%")]
        public float Factor = 1.0f;

        public abstract IMultiplierEffect CreateRuntimeEffect();
    }
}
