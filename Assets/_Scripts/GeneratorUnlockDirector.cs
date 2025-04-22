using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratorUnlockDirector : MonoBehaviour
{
    [SerializeField] private List<Generator> _generators = new();

    private void Start()
    {
        Generator.OnGeneratorLeveledUp += (sender, args) =>
        {
            Generator nextTierGenerator = _generators.Find(g => g.GetTier() == args.Generator.GetTier() + 1);

            if (nextTierGenerator != null && !nextTierGenerator.isActiveAndEnabled)
            {
                if (args.Generator.GetLevel() >= nextTierGenerator.GetPreviousTierGeneratorsThreshold())
                    nextTierGenerator.gameObject.SetActive(true);
            }
        };
    }
}
