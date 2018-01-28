using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private Canvas _canvas;
    private Text _moneyText;

    private MoneyManager money;
    private Controller player;
    private GameSpawner spawner;
    private Canvas environmentCanvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _moneyText = transform.Find("Panel").Find("You Made Text").GetComponent<Text>();

        money = GameObject.FindObjectOfType<MoneyManager>();
        player = GameObject.FindObjectOfType<Controller>();
        spawner = GameObject.FindObjectOfType<GameSpawner>();
        environmentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void ShowWinScreen(float moneyToWin)
    {
        _moneyText.text = "You made $" + (int)moneyToWin;
        _canvas.enabled = true;

        player.enabled = false;
        spawner.enabled = false;
        money.enabled = false;
        environmentCanvas.enabled = false;
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
