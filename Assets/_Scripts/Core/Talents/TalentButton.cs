using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class TalentButton : MonoBehaviour
    {
        [Header("Talent")]
        [SerializeField] private TalentDefinition _talent;

        [Header("Prerequisites")]
        [SerializeField] private TalentButton[] _prerequisites;

        [Header("UI Elements")]
        [SerializeField] private Image _icon;
        [SerializeField] private Image _border;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private GameObject _lockPanel;

        [Inject] private Bank _bank;

        private int _prerequisitesMet = 0;
        private bool _used;
        private bool _locked;

        public Action OnTalentUsed;

        public void Setup()
        {
            if (_prerequisites.Length > 0)
                SetLockState(true);
            else
                SetLockState(false);

            foreach (var requisite in _prerequisites)
            {
                requisite.OnTalentUsed += () =>
                {
                    _prerequisitesMet++;

                    if (_prerequisitesMet == _prerequisites.Length)
                        SetLockState(false);
                };
            }

            _icon.sprite = _talent.Icon;
            _costText.SetText(_talent.Cost.ToString());
        }

        public void UseTalent()
        {
            if (_locked || _used || !PrerequsitesMet() || !_bank.HasEnough(_talent.Cost)) return;

            BuyTalent();
            ExecuteEffect();
        }

        private void BuyTalent()
        {
            _bank.RemoveCurrency(_talent.Cost);
        }

        public void ExecuteEffect()
        {
            _talent.Execute();

            SetUsed(true);

            OnTalentUsed?.Invoke();
        }

        private bool PrerequsitesMet()
        {
           return _prerequisites.All(prerequisite => prerequisite.WasUsed());
        }

        public string GetTalentName() => _talent.name;

        public bool WasUsed() => _used;

        public void SetUsed(bool used)
        {
            _used = used;

            UpdateUsedStateVisuals();
        }

        private void UpdateUsedStateVisuals()
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

        private void SetLockState(bool state)
        {
            _locked = state;

            UpdateLockedStateVisuals();
        }

        private void UpdateLockedStateVisuals()
        {
            _lockPanel.SetActive(_locked);
        }
    }
}
