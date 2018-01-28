using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager I { get; private set; }

    public float Money;
    public float MoneyToWin;
    public float MoneyToLose;
    public float MoneyPerShippedGame;
    public float LostMoneyPerSecond;
    public float LoseMoneyTimer;
    public MoneyDisplay MoneyDisplay;

    private WinScreen _winScreen;
    private GameOverScreen _gameOverScreen;

    private void Awake()
    {
        I = this;

        _winScreen = GameObject.Find("Win Screen").GetComponent<WinScreen>();
        _gameOverScreen = GameObject.Find("Game Over Screen").GetComponent<GameOverScreen>();
    }

    private void Start()
    {
        StartCoroutine(LoseMoney());
    }

    private void Update()
    {
        if (Money >= MoneyToWin)
        {
            _winScreen.ShowWinScreen(MoneyToWin);
        }
        else if (Money <= MoneyToLose)
        {
            _gameOverScreen.ShowGameOverScreen();
        }    
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        MoneyDisplay.I.HideText();
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

    public void AddMoney(float value)
    {
        Money += value > 0 ? value : 0f;
    }
}
