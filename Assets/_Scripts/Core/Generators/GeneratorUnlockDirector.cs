using UnityEngine;
using Zenject;

namespace Core.Generators
{
    public class GeneratorUnlockDirector : MonoBehaviour
    {
        [Inject] private GeneratorsController _generatorsController;

        private void Start()
        {
            Generator.OnGeneratorLeveledUp += (sender, args) =>
            {
                IGenerator nextTierGenerator = _generatorsController.GetAllGenerators().Find(g => g.GetTier() == args.Generator.GetTier() + 1);

                if (nextTierGenerator != null && !nextTierGenerator.GetState())
                {
                    if (args.Generator.GetLevel() >= nextTierGenerator.GetPreviousTierGeneratorsThreshold())
                        nextTierGenerator.SetState(true);
                }
            };
        }
    }
}
