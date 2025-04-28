using UnityEngine;

namespace Core
{
    public abstract class TalentDefinition : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;

        public abstract void ExecuteEffect();
    }
}
