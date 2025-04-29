using UnityEngine;

namespace Core
{
    public abstract class ConsumableDefinition : ScriptableObject, IItem
    {
        public int ID;

        public string Description;

        public Sprite Icon;

        public abstract void Use();

        public int GetID() => ID;

        public string GetName() => name;

        public string GetDescription() => Description;

        public Sprite GetIcon() => Icon;
    }
}
