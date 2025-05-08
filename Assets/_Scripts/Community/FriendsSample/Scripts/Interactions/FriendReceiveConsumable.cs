using Core;
using Unity.Services.Friends;
using UnityEngine;
using Zenject;

namespace Community.Friends.Interactions
{
    public class FriendReceiveConsumable : MonoBehaviour
    {
        [SerializeField] private ConsumableDefinition _consumable;

        [Inject] private Inventory _inventory;

        public void Setup()
        {
            FriendsService.Instance.MessageReceived += e =>
            {
                var consumableMsg = e.GetAs<Message>();

                if (consumableMsg.Type != "Consumable")
                    return;

                _inventory.AddItem(_consumable);
            };
        }
    }
}
