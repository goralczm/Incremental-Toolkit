using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratorsController : MonoBehaviour
{
    private List<Generator> _allGenerators = null;
    private List<Generator> CacheGenerators() => _allGenerators = FindObjectsByType<Generator>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

    public List<Generator> GetGenerators() => _allGenerators ??= CacheGenerators();
}
