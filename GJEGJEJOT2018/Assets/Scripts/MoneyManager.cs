using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager I { get; private set; }

    public Text MoneyText;

    public float Money;
    public float LostMoneyPerSecond;
    public float LoseMoneyTimer;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        StartCoroutine(LoseMoney());
    }

    private IEnumerator LoseMoney()
    {
        while (true)
        {
            Money -= LostMoneyPerSecond * LoseMoneyTimer;
            MoneyText.text = "$" + (int)Money;
            yield return new WaitForSeconds(LoseMoneyTimer);
        }
        
    }
}
