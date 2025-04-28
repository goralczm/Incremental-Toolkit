using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class TalentsController : MonoBehaviour
    {
        private List<TalentButton> _talentButtons = new();

        private void Awake()
        {
            _talentButtons = FindObjectsByType<TalentButton>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            Bank.OnPrestige += (sender, args) =>
            {
                foreach (var talentButton in _talentButtons)
                {
                    talentButton.ResetButton();
                }
            };
        }
    }
}
