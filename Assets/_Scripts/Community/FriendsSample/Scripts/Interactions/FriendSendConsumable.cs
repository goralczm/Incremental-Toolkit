using System;
using Unity.Services.Friends;
using UnityEngine;
using UnityEngine.UI;

public class FriendSendConsumable : MonoBehaviour
{
    [SerializeField] private string _friendId;
    [SerializeField] private Button _button;

    private const string LastSendTimeKey = "LastConsumableSendTime";
    private const int CooldownDurationSeconds = 45; // 12 hours 

    private void Awake()
    {
        UpdateButtonStatus();

        print(GetSecondsLeft());
        Invoke("UpdateButtonStatus", GetSecondsLeft());
    }

    public void SetFriendId(string friendId)
    {
        _friendId = friendId;
    }

    public async void SendButtonClicked()
    {
        if (!CanSendConsumable()) return;

        await FriendsService.Instance.MessageAsync(_friendId, new Message { Type = "Consumable", Payload = "Receive consumable" });

        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        PlayerPrefs.SetString($"{LastSendTimeKey}{_friendId}", currentUnixTime.ToString());
        PlayerPrefs.Save();

        _button.interactable = false;
        Invoke("UpdateButtonStatus", CooldownDurationSeconds);
    }

    private bool CanSendConsumable()
    {
        if (!PlayerPrefs.HasKey($"{LastSendTimeKey}{_friendId}"))
            return true;

        long lastSendTime = long.Parse(PlayerPrefs.GetString($"{LastSendTimeKey}{_friendId}"));
        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        return (currentUnixTime - lastSendTime) >= CooldownDurationSeconds;
    }

    private void UpdateButtonStatus()
    {
        _button.interactable = CanSendConsumable();
    }

    private long GetSecondsLeft()
    {
        if (!PlayerPrefs.HasKey($"{LastSendTimeKey}{_friendId}"))
            return 0;

        long lastSendTime = long.Parse(PlayerPrefs.GetString($"{LastSendTimeKey}{_friendId}"));
        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long elapsed = currentUnixTime - lastSendTime;

        int remaining = CooldownDurationSeconds - (int)elapsed;
        return Mathf.Max(0, remaining);
    }
}
