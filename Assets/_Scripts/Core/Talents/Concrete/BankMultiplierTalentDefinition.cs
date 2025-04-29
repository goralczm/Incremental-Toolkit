using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Talents/Bank Multiplier Talent Definition", fileName = "Bank Upgrade")]
    public class BankMultiplierTalentDefinition : TalentDefinition
    {
        public float Factor = 1.05f;

        public override void Execute()
        {
            FindFirstObjectByType<Bank>().AddEffect(new PermanentMultiplier(Factor));
        }
    }
}
