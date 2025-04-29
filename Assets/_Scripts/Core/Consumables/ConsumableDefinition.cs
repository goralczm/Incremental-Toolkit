using UnityEngine;

namespace Core
{
    public abstract class ConsumableDefinition : ScriptableObject, IItem
    {
        public string Description;
        public Sprite Icon;

        public abstract void Use();

        public string GetName() => name;

        public string GetDescription() => Description;

        public Sprite GetIcon() => Icon;
    }
}
