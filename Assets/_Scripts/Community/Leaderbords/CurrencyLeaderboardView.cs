using Community;
using Core.Utility;
using System;
using System.Text;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using Utilities;

public class CurrencyLeaderboardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private void Awake()
    {
        CurrencyLeaderboard.OnLeaderboardInitialized += (LeaderboardScoresPage scores) =>
        {
            StringBuilder output = new StringBuilder();

            int i = 1;
            foreach (var score in scores.Results)
            {
                output.Append("#");
                output.Append(i);
                output.Append(" ");
                output.Append(score.PlayerName.Substring(0, 8));
                if (score.PlayerName.Length > 8)
                    output.Append("...");
                output.Append(": ");
                output.Append(ParseScore(score.Score));
                output.AppendLine();
                i++;
            }

            _text.SetText(output.ToString());
        };
    }

    private string ParseScore(double score)
    {
        string postfix = "";

        float castedScore = Convert.ToSingle(score);
        if (castedScore > 1000)
        {
            castedScore /= 1000f;
            postfix = "K";
        }

        return $"{castedScore.LimitDecimalPoints(2)}{postfix}";
    }
}
