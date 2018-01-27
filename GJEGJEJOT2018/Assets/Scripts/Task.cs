﻿using System;
using UnityEngine;

[Serializable]
public class Task
{
    private const float speedModifier = .015f;

    public float percent;

    public Task(int value)
    {
        percent = (float)value / 100f;
    }

    public void LowerPercent(float value)
    {
        percent = Mathf.Clamp01(percent - value * Time.deltaTime * speedModifier);
    }
}
