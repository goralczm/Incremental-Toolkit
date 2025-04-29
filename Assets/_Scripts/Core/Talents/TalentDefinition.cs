using UnityEngine;

namespace Core
{
    public abstract class TalentDefinition : ScriptableObject
    {
        public string Description;
        
        public Sprite Icon;

        public int Cost;

        public abstract void Execute();
    }
}
