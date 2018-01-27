using System;
using UnityEngine;

[Serializable]
public class Task
{
    private const float speedModifier = .03f;

    public static Color[] colors =
    {
        new Color(.11f, 0.3f, 1f),
        new Color(.85f, .07f, .07f),
        new Color(.99f, .78f, .1f),
        new Color(.11f, .87f, .29f)
    };

    public static Color GetColor(TaskEnum task)
    {
        if ((int)task >= colors.Length) return Color.white;
        return colors[(int)task];
    }

    public float percent;

    public Task(int value)
    {
        percent = (float)value / 100f;
    }

    public void LowerPercent(float value)
    {
        percent = Mathf.Clamp01(percent - value * Time.deltaTime * speedModifier);
    }

    public bool IsEnded()
    {
        return percent == 0f;
    }
}
