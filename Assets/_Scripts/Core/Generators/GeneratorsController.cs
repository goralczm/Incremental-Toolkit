using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Generators
{
    public class GeneratorsController : MonoBehaviour
    {
        private List<IGenerator> _activeGenerators = new();
        private List<IGenerator> _allGenerators = new();

        public List<IGenerator> GetActiveGenerators() => _activeGenerators;

        private float _cachedProduction;

        private Dictionary<int, IGenerator> _cachedGeneratorsByTier = new();

        private void Awake()
        {
            _allGenerators = FindObjectsByType<Generator>(FindObjectsInactive.Include, FindObjectsSortMode.None).Select(g => g as IGenerator).ToList();

            Generator.OnGeneratorChangedState += (sender, args) =>
            {
                if (args.IsActive)
                {
                    if (!_activeGenerators.Contains(args.Generator))
                        _activeGenerators.Add(args.Generator);
                }
                else
                    _activeGenerators.Remove(args.Generator);
            };
        }

        public float GetActiveGeneratorsProduction() => GetActiveGenerators().Sum(g => g.GetProduction());

        public List<IGenerator> GetAllGenerators() => _allGenerators;

        public IGenerator GetGeneratorByTier(int tier)
        {
            if (_cachedGeneratorsByTier.TryGetValue(tier, out var generator))
                return generator;

            generator = _allGenerators.Find(g => g.GetTier() == tier);
            _cachedGeneratorsByTier.Add(tier, generator);
            return generator;
        }
    }
}