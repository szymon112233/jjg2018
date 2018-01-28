using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay I { get; private set; }
    public Text MoneyText;

    public Color goodColor;
    public Color badColor;

    private void Awake()
    {
        I = this;
    }

    public void UpdateMoneyText(float value)
    {
        MoneyText.text = "$" + (int)value;
        if (value > 0f) MoneyText.color = goodColor;
        else MoneyText.color = badColor;
    }

    public void HideText()
    {
        MoneyText.text = "";
    }
}
