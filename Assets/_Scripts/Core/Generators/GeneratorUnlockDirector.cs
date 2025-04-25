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
                Generator nextTierGenerator = _generatorsController.GetAllGenerators().Find(g => g.GetTier() == args.Generator.GetTier() + 1);

                if (nextTierGenerator != null && !nextTierGenerator.isActiveAndEnabled)
                {
                    if (args.Generator.GetLevel() >= nextTierGenerator.GetPreviousTierGeneratorsThreshold())
                        nextTierGenerator.gameObject.SetActive(true);
                }
            };
        }
    }
}
