using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager I { get; private set; }

    public float Money;
    public float MoneyPerShippedGame;
    public float LostMoneyPerSecond;
    public float LoseMoneyTimer;
    public MoneyDisplay MoneyDisplay;

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
            MoneyDisplay.I.UpdateMoneyText(Money);
            yield return new WaitForSeconds(LoseMoneyTimer);
        }
        
    }
}
