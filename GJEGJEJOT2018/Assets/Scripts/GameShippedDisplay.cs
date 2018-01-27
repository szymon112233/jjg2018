using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShippedDisplay : MonoBehaviour
{
    public Text GameTitle;
    public Text GameScore;
    public Text EarnedMoney;
    public Canvas Canvas;

    private enum Critics { Ign, Gamespot, Totalbiscuit, Polygon, Kotaku }

    private void Awake()
    {
        Canvas.enabled = false;
    }

    public void ShowShipMessage(string title, float completion, float earnedMoney)
    {
        Canvas.enabled = true;

        string criticsMessage = RandomizeScoreText(completion);
        GameTitle.text = title + " has been shipped!";
        GameScore.text = "Critics say: " + criticsMessage;
        EarnedMoney.text = "We made " + earnedMoney + " bucks!";

        StartCoroutine(ShowDisplay());
    }

    private string RandomizeScoreText(float completion)
    {
        Critics critic = (Critics)Random.Range(0, 4);

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
        Color color = new Color(0f, 0f, 0f, 0f); 
        while (color.a < 1f)
        {
            GameTitle.color = color;
            GameScore.color = color;
            EarnedMoney.color = color;
            yield return new WaitForSeconds(0.03f);
            color.a += 0.1f;
        }
        yield return new WaitForSeconds(4f);
        while (color.a > 0f)
        {
            GameTitle.color = color;
            GameScore.color = color;
            EarnedMoney.color = color;
            yield return new WaitForSeconds(0.03f);
            color.a -= 0.1f;
        }
        Canvas.enabled = false;
    }
}
