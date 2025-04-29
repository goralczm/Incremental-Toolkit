using Core.Generators;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Consumables/Temporary Multiplier Consumable", fileName = "New Temporary Multiplier Consumable")]
    public class TemporaryMultiplierConsumableDefinition : ConsumableDefinition
    {
        public int Tier;
        public float Factor;
        public float Duration;
        
        public override void Use()
        {
            FindFirstObjectByType<GeneratorsController>().GetGeneratorByTier(Tier).AddEffect(new TemporaryMultiplier(Factor, Duration));
        }
    }
}