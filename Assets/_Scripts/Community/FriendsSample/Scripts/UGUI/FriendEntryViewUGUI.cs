using TMPro;
using Unity.Services.Friends.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Services.Samples.Friends.UGUI
{
    public class FriendEntryViewUGUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_NameText = null;
        [SerializeField] TextMeshProUGUI m_ActivityText = null;
        [SerializeField] Image m_PresenceColorImage = null;
        [SerializeField] FriendSendConsumable m_FriendSendConsumablePrefab = null;

        public Button removeFriendButton = null;
        public Button blockFriendButton = null;

        public void Init(string playerId, string playerName, Availability presenceAvailabilityOptions, string activity)
        {
            m_NameText.text = playerName;
            var index = (int)presenceAvailabilityOptions - 1;
            var presenceColor = ColorUtils.GetPresenceColor(index);
            m_PresenceColorImage.color = presenceColor;
            m_ActivityText.text = activity;
            m_FriendSendConsumablePrefab.SetFriendId(playerId);
        }
    }
}