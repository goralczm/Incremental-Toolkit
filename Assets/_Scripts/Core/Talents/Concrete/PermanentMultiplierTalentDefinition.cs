using Core.Generators;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Talents/Multiplier Talent", fileName = "Multiplier Talent")]
    public class PermanentMultiplierTalentDefinition : TalentDefinition
    {
        public int Tier = 1;
        public float Factor = 1f;

        public override void ExecuteEffect()
        {
            FindFirstObjectByType<GeneratorsController>().GetGeneratorByTier(Tier)?.AddEffect(new PermanentMultiplier(Factor));
        }
    }
}
