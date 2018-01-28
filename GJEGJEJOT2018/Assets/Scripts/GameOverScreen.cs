using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private Canvas _canvas;
    //private Text _moneyText;

    private MoneyManager money;
    private Controller player;
    private GameSpawner spawner;
    private Canvas environmentCanvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        //_moneyText = transform.Find("Panel").Find("Bankrupt Text").GetComponent<Text>();

        money = GameObject.FindObjectOfType<MoneyManager>();
        player = GameObject.FindObjectOfType<Controller>();
        spawner = GameObject.FindObjectOfType<GameSpawner>();
        environmentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void ShowGameOverScreen()
    {
        //_moneyText.text = ""
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
