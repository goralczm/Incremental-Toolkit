using Community;
using System.Text;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

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
                output.Append(score.Score.ToString());
                output.AppendLine();
                i++;
            }

            _text.SetText(output.ToString());
        };
    }
}
