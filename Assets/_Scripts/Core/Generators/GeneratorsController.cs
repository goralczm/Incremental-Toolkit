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

                _cachedProduction = GetActiveGenerators().Sum(g => g.GetProduction());
            };

            Generator.OnGeneratorLeveledUp += (sender, args) =>
            {
                _cachedProduction = GetActiveGenerators().Sum(g => g.GetProduction());
            };
        }

        public float GetActiveGeneratorsProduction() => _cachedProduction;
        
        public List<IGenerator> GetAllGenerators() => _allGenerators;
    }
}