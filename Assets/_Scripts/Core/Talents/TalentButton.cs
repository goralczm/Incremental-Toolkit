using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TalentButton : MonoBehaviour
    {
        [SerializeField] private TalentDefinition _talent;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _border;

        private bool _used;

        private void Start()
        {
            _icon.sprite = _talent.Icon;
        }

        public void UseTalent()
        {
            if (_used) return;

            _talent.ExecuteEffect();
            SetUsed(true);
        }

        public string GetTalentName() => _talent.Name;

        public bool WasUsed() => _used;

        public void SetUsed(bool used)
        {
            _used = used;

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (_used)
                _border.color = Color.green;
            else
                _border.color = Color.gray;
        }

        public void ResetButton()
        {
            SetUsed(false);
        }
    }
}
