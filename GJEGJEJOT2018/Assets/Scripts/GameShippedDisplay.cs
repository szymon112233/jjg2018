﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShippedDisplay : MonoBehaviour
{
    public Text GameTitle;
    public Text GameScore;
    public Text EarnedMoney;
    public Canvas Canvas;

    private Color currentGameColor;

    private enum Critics { Ign, Gamespot, Totalbiscuit, Polygon, Kotaku }

    private void Awake()
    {
        Canvas.enabled = false;
    }

    public void ShowSuccessfulShipMessage(string title, Color color, float completion, float earnedMoney)
    {
        StopAllCoroutines();
        Canvas.enabled = true;

        string criticsMessage = RandomizeScoreText(completion);
        GameTitle.text = title + " has been shipped!";
        GameTitle.color = color;
        currentGameColor = color;
        GameScore.text = "Critics say: " + criticsMessage;
        EarnedMoney.text = "We made " + (int)earnedMoney + " bucks!";

        StartCoroutine(ShowDisplay());
    }

    public void ShowUnsuccessfulShipMessage(string title, Color color, float completion, float earnedMoney)
    {
        StopAllCoroutines();
        Canvas.enabled = true;

        string criticsMessage = RandomizeScoreText(completion);
        GameTitle.text = title + " has been shipped!";
        GameTitle.color = color;
        currentGameColor = color;
        GameScore.text = "Critics say: " + criticsMessage;
        EarnedMoney.text = "We made just $" + (int)earnedMoney + ". Welp. Maybe next time";

        StartCoroutine(ShowDisplay());
    }

    public void ShowFailMessage(string title, Color color, float completion)
    {
        StopAllCoroutines();
        Canvas.enabled = true;

        string criticsMessage = RandomizeScoreText(completion);
        GameTitle.text = title + " has failed!";
        GameTitle.color = color;
        currentGameColor = color;
        GameScore.text = "Critics didn't like this one: " + criticsMessage;
        EarnedMoney.text = "We're filled with grief";

        StartCoroutine(ShowDisplay());
    }

    private string RandomizeScoreText(float completion)
    {
        // TODO: Replace placeholders, set correct range
        Critics critic = (Critics)Random.Range(0, 0);

        switch(critic)
        {
            case Critics.Ign:
                int score = Mathf.RoundToInt(completion * 10);
                return score + "/10 - IGN";
            case Critics.Gamespot:
                return "placeholder";
            case Critics.Totalbiscuit:
                return "placeholder";
            case Critics.Polygon:
                return "placeholder";
            case Critics.Kotaku:
                return "placeholder";
            default:
                return "welp";
        }
    }

    private IEnumerator ShowDisplay()
    {
        Color color = new Color(1f, 1f, 1f, 0f);
        Color gameColor = currentGameColor;
        while (color.a < 1f)
        {
            color.a += 0.1f;
            GameTitle.color = color * gameColor;
            GameScore.color = color;
            EarnedMoney.color = color;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(4f);
        while (color.a > 0f)
        {
            color.a -= 0.1f;
            GameTitle.color = color * gameColor;
            GameScore.color = color;
            EarnedMoney.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        Canvas.enabled = false;
    }
}
