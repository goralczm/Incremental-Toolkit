using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Generators
{
    public class GeneratorsController : MonoBehaviour
    {
        private List<Generator> _activeGenerators = new();
        private List<Generator> _allGenerators = new();

        public List<Generator> GetActiveGenerators() => _activeGenerators;

        private float _cachedProduction;

        private void Awake()
        {
            _allGenerators = FindObjectsByType<Generator>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            Generator.OnGeneratorChangedState += (sender, args) =>
            {
                if (args.IsActive)
                    _activeGenerators.Add(args.Generator);
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
        
        public List<Generator> GetAllGenerators() => _allGenerators;
    }
}