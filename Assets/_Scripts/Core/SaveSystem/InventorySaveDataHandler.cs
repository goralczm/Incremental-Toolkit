using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core
{
    public class InventorySaveDataHandler : MonoBehaviour, ISavable
    {
        [Inject] private Inventory _inventory;

        private void Awake()
        {
            GameProgression.OnProgressLoaded += (sender, args) =>
            {
                if (args.Data.TryGetData("Items IDs", out var items))
                {
                    object[] allObjects = Resources.LoadAll("Items");
                    IItem[] allItems = allObjects.Select(x => x as IItem).ToArray();

                    List<int> savedItems = (List<int>)items;

                    foreach (var itemID in savedItems)
                    {
                        IItem foundItem = allItems.First(i => i.GetID().Equals(itemID));
                        if (foundItem != null)
                            _inventory.AddItem(foundItem);
                    }
                }
            };

            GameProgression.DataToSave.Add(this);
        }

        public List<(string, object)> SaveData()
        {
            return new List<(string, object)> { ("Items IDs", _inventory.GetItems().Select(i => i.GetID()).ToList()) };
        }
    }
}
