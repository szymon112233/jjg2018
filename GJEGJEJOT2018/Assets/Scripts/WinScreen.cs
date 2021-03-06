﻿using System.Collections;
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
    //private Canvas environmentCanvas;
    private Button button;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _moneyText = transform.Find("Panel").Find("You Made Text").GetComponent<Text>();

        money = GameObject.FindObjectOfType<MoneyManager>();
        player = GameObject.FindObjectOfType<Controller>();
        spawner = GameObject.FindObjectOfType<GameSpawner>();

        button = GetComponentInChildren<Button>();
        //environmentCanvas = GameObject.Find("World UI").GetComponent<Canvas>();
    }

    public void ShowWinScreen(float moneyToWin)
    {
        button.Select();
        _moneyText.text = "You made $" + (int)moneyToWin;
        _canvas.enabled = true;

        player.enabled = false;
        spawner.enabled = false;
        money.enabled = false;
        foreach (GameObject bars in GameObject.FindGameObjectsWithTag("Employee Bars"))
        {
            Destroy(bars);
        }
        //environmentCanvas.enabled = false;
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
