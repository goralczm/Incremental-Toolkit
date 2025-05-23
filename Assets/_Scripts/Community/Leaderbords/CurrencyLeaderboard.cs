using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using Zenject;

namespace Community
{
    public class CurrencyLeaderboard : MonoBehaviour
    {
        const string LeaderboardId = "Currency";

        [SerializeField] private GameObject _panel;

        [Inject] private Statistics _statistics;

        string VersionId { get; set; }
        int Offset { get; set; }
        int Limit { get; set; }
        int RangeLimit { get; set; }
        List<string> FriendIds { get; set; }

        public static Action<LeaderboardScoresPage> OnLeaderboardInitialized;

        private float _cooldown;

        public async void Setup()
        {
            InvokeRepeating("UpdateLeaderboard", 0, 15f); // TODO: figure out good heart beat
        }

        private void OnEnable()
        {
            UpdateLeaderboard();
        }

        public async void UpdateLeaderboard()
        {
            if (Time.time < _cooldown) return;

            await AddScore();
            print("Score saved!");

            if (!_panel.activeSelf) return;

            _cooldown = Time.time + 15f;

            try
            {
                LeaderboardScoresPage scores = await GetScores();
                OnLeaderboardInitialized?.Invoke(scores);
                print("Leaderboard updated!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error fetching leaderboard scores: {e.Message}");
            }
        }

        public async Task AddScore()
        {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, _statistics.GetTotalEarned());
            //Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public async Task<LeaderboardScoresPage> GetScores()
        {
            LeaderboardScoresPage scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));

            return scoresResponse;
        }

        public async void GetPaginatedScores()
        {
            Offset = 10;
            Limit = 10;
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        }

        public async void GetPlayerScore()
        {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }

        public async void GetVersionScores()
        {
            var versionScoresResponse =
                await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
            Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
        }
    }
}
