using System;
using UnityEngine;

[Serializable]
public class Ability
{
    [SerializeField] [Range(0, 1)] private float downRange;
    [SerializeField] [Range(0, 1)] private float upRange;
    private float value;

    public float Value
    {
        get { return value; }
        private set { this.value = value; }
    }

    public void GenerateValue()
    {
        Value = UnityEngine.Random.Range(downRange, upRange);
    }
}
