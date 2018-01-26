using System;
using UnityEngine;

[Serializable]
public class Task
{
    private const float speedModifier = .015f;

    public float percent;

    public Task(float value)
    {
        percent = value;
    }

    public void LowerPercent(float value)
    {
        percent = Mathf.Clamp01(percent - value * Time.deltaTime * speedModifier);
    }
}
