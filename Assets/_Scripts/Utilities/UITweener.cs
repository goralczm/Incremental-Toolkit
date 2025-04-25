using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Utilities
{
    public enum UIAnimationType
    {
        Move,
        Scale,
        Fade,
        Rotation,
    }

    public class UITweener : MonoBehaviour
    {
        public bool isBusy;

        public UIAnimationType animationType;
        public Ease easeType;
        public float duration;
        public float delay;
        public int loopsCount;
        public bool startReversed;
        public bool hasSetup;
        public bool cannotInterupt;
        public bool startAwake;

        public Vector3 startValue;
        public Vector3 finalValue;

        public UnityEvent onShowCompleteCallback;
        public UnityEvent onHideCompleteCallback;

        public bool isReversed;
        private RectTransform _rect;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            if (startAwake)
                Toggle();
        }

        public void Setup()
        {
            if (hasSetup)
                return;

            _rect = GetComponent<RectTransform>();

            switch (animationType)
            {
                case UIAnimationType.Move:
                    if (!startReversed)
                        _rect.anchoredPosition = startValue;
                    else
                        _rect.anchoredPosition = finalValue;
                    break;
                case UIAnimationType.Scale:
                    if (!startReversed)
                        _rect.localScale = startValue;
                    else
                        _rect.localScale = finalValue;
                    break;
                case UIAnimationType.Fade:
                    _canvasGroup = GetComponent<CanvasGroup>();
                    if (_canvasGroup == null)
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    if (!startReversed)
                        _canvasGroup.alpha = startValue.x;
                    else
                        _canvasGroup.alpha = finalValue.x;
                    break;
            }

            hasSetup = true;
        }

        [ContextMenu("Show")]
        public void Show()
        {
            isReversed = false;
            HandleTween();
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            isReversed = true;
            HandleTween();
        }

        [ContextMenu("Toggle")]
        public void Toggle()
        {
            isReversed = !isReversed;
            HandleTween();
        }

        private void HandleTween()
        {
            if (cannotInterupt && isBusy)
                return;

            isBusy = true;
            Setup();

            if (!isReversed)
                gameObject.SetActive(true);

            switch (animationType)
            {
                case UIAnimationType.Fade:
                    Fade();
                    break;
                case UIAnimationType.Move:
                    Move();
                    break;
                case UIAnimationType.Scale:
                    Scale();
                    break;
                case UIAnimationType.Rotation:
                    Rotate();
                    break;
            }
        }

        private void OnShowComplete()
        {
            isBusy = false;
            onShowCompleteCallback?.Invoke();
        }

        private void OnHideComplete()
        {
            isBusy = false;
            onHideCompleteCallback?.Invoke();
        }

        private void Fade()
        {
            float targetFade = isReversed ? startValue.x : finalValue.x;

            var tween = _canvasGroup.DOFade(targetFade, duration)
                                    .SetDelay(delay)
                                    .SetEase(easeType)
                                    .SetUpdate(true);

            if (loopsCount != 0)
                tween.SetLoops(loopsCount);

            if (isReversed)
                tween.onComplete += OnHideComplete;
            else
                tween.onComplete += OnShowComplete;

            tween.id = this;
        }

        private void Move()
        {
            Vector2 targetPos = isReversed ? startValue : finalValue;

            var tween = _rect.DOAnchorPos(targetPos, duration)
                             .SetDelay(delay)
                             .SetEase(easeType)
                             .SetUpdate(true);

            if (loopsCount != 0)
                tween.SetLoops(loopsCount, LoopType.Yoyo);

            if (isReversed)
                tween.onComplete += OnHideComplete;
            else
                tween.onComplete += OnShowComplete;

            tween.id = this;
        }

        private void Scale()
        {
            Vector2 targetScale = isReversed ? startValue : finalValue;

            var tween = transform.DOScale(targetScale, duration)
                                 .SetDelay(delay)
                                 .SetEase(easeType)
                                 .SetUpdate(true);

            if (loopsCount != 0)
                tween.SetLoops(loopsCount);

            if (isReversed)
                tween.onComplete += OnHideComplete;
            else
                tween.onComplete += OnShowComplete;

            tween.id = this;
        }

        private void Rotate()
        {
            Vector3 targetRotation = isReversed ? startValue : finalValue;

            var tween = transform.DORotate(targetRotation, duration)
                                 .SetDelay(delay)
                                 .SetEase(easeType)
                                 .SetUpdate(true);

            if (loopsCount != 0)
                tween.SetLoops(loopsCount);

            if (isReversed)
                tween.onComplete += OnHideComplete;
            else
                tween.onComplete += OnShowComplete;

            tween.id = this;
        }

        public void CancelTween()
        {
            this.DOKill();
        }
    }
}
