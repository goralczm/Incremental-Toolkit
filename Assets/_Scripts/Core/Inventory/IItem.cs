using UnityEngine;

namespace Core
{
    public interface IItem
    {
        public int GetID();

        public string GetName();
        
        public string GetDescription();
        
        public Sprite GetIcon();

        public void Use();
    }
}
