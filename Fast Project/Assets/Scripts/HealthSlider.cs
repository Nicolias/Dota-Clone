using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : SerializedMonoBehaviour
{
    [SerializeField] private ITarget _objectWhithHealth;
    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        _healthSlider.minValue = 0;
        _healthSlider.maxValue = _objectWhithHealth.Health;
        _healthSlider.value = _objectWhithHealth.Health;
    }

    private void OnEnable()
    {
        _objectWhithHealth.OnHealthChanged += ChangeHealthSliderValue;
    }

    private void OnDisable()
    {
        _objectWhithHealth.OnHealthChanged -= ChangeHealthSliderValue;
    }

    private void ChangeHealthSliderValue(int healthValue)
    {
        if (healthValue > _healthSlider.maxValue) throw new InvalidProgramException();

        _healthSlider.value = healthValue;
    }
}
