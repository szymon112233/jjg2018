using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay I { get; private set; }
    public Text MoneyText;

    private void Awake()
    {
        I = this;
    }

    public void UpdateMoneyText(float value)
    {
        MoneyText.text = "$" + (int)value;
    }

    public void HideText()
    {
        MoneyText.text = "";
    }
}
