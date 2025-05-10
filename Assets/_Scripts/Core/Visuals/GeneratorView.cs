using UnityEngine;
using DG;
using DG.Tweening;
using Zenject;
using Core.Generators;
using System;

namespace Core.Visuals
{
    public class GeneratorView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _spacing = 5f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        [Header("Instances")]
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private GameObject _rightArrow;

        [Inject] private GeneratorsController _generatorsController;

        private int _currentGeneratorIndex = 0;

        private Vector2 _startPos;
        private float _cooldownTime;

        public Action<IGenerator> OnViewChanged;

        private void Start()
        {
            _startPos = transform.localPosition;

            Generator.OnGeneratorChangedState += (sender, args) =>
            {
                UpdateButtonsVisibility();
                MoveToTargetPosition();
            };

            UpdateButtonsVisibility();
        }

        public void MoveRight()
        {
            if (Time.time < _cooldownTime) return;

            _currentGeneratorIndex++;

            UpdateButtonsVisibility();
            MoveToTargetPosition();

            OnViewChanged?.Invoke(GetCurrentGeneratorInView());
        }

        public void MoveLeft()
        {
            if (Time.time < _cooldownTime) return;

            _currentGeneratorIndex--;

            UpdateButtonsVisibility();
            MoveToTargetPosition();

            OnViewChanged?.Invoke(GetCurrentGeneratorInView());
        }

        private void MoveToTargetPosition()
        {
            transform.DOLocalMove(_startPos - new Vector2(_spacing * _currentGeneratorIndex, 0), _speed)
                .SetEase(_ease);

            _cooldownTime = Time.time + _speed;
        }

        private void UpdateButtonsVisibility()
        {
            int generatorsCount = _generatorsController.GetActiveGenerators().Count;
            _currentGeneratorIndex = Mathf.Clamp(_currentGeneratorIndex, 0, generatorsCount - 1);

            if (_leftButton != null)
                _leftButton.SetActive(true);

            if (_rightArrow != null)
                _rightArrow.SetActive(true);

            if (_currentGeneratorIndex == generatorsCount - 1)
            {
                if (_rightArrow != null)
                    _rightArrow.SetActive(false);
            }

            if (_currentGeneratorIndex == 0)
            {
                if (_leftButton != null)
                    _leftButton.SetActive(false);
            }
        }

        public IGenerator GetCurrentGeneratorInView()
        {
            if (_generatorsController.GetActiveGenerators().Count == 0)
                return null;

            return _generatorsController.GetActiveGenerators()[_currentGeneratorIndex];
        }
    }
}
