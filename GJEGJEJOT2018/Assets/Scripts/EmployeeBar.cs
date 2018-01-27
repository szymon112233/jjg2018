﻿using UnityEngine;
using UnityEngine.UI;

public class EmployeeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillBar;

    public void ChangeValue(float value)
    {
        fillBar.enabled = true;
        slider.value = value;

        if (value == 0f)
        {
            RemoveFill();
        }
            
    }

    private void RemoveFill()
    {
        fillBar.enabled = false;
    }
}