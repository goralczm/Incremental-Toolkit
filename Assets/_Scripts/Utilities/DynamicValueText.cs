using TMPro;
using UnityEngine;

public class DynamicValueText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _changeIntervals = .03f;

    [Header("Instances")]
    [SerializeField] private TextMeshProUGUI _text;

    private int _currentVisualValue;
    private float _lastTimeTick;
    private int _value;

    private void Update()
    {
        if (_currentVisualValue == _value)
            return;

        if (Time.time - _lastTimeTick >= _changeIntervals)
            Tick();
    }

    private void Tick()
    {
        CalculateValue();
        _text.SetText(GetDisplayValue(_currentVisualValue.ToString()));
        _lastTimeTick = Time.time;
    }

    private void CalculateValue()
    {
        int diff = Mathf.Abs(_value - _currentVisualValue);

        int decimalPlaces = -1;
        while (diff > 0)
        {
            diff /= 10;
            decimalPlaces++;
        }

        if (_value > _currentVisualValue)
            _currentVisualValue += (int)Mathf.Pow(10, decimalPlaces);
        else
            _currentVisualValue -= (int)Mathf.Pow(10, decimalPlaces);
    }

    public void SetValue(float value)
    {
        _value = (int)value;
    }

    public virtual string GetDisplayValue(string value)
    {
        return value;
    }
}
