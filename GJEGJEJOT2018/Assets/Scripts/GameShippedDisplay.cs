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

    private Color currentGameColor;

    private enum Critics { Ign, Eurogamer, Giantbomb, Totalbiscuit, Steam }

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
        GameScore.text = criticsMessage;
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
        GameScore.text = criticsMessage;
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
        GameScore.text = criticsMessage;
        EarnedMoney.text = "We're filled with grief";

        StartCoroutine(ShowDisplay());
    }

    private string RandomizeScoreText(float completion)
    {
        // TODO: Replace placeholders, set correct range
        Critics critic = (Critics)Random.Range(0, 4);
        int score = 0;
        switch(critic)
        {
            case Critics.Ign:
                score = Mathf.RoundToInt(completion * 10);
                return score + "/10 - IGN";
            case Critics.Eurogamer:
                if (completion < 5f) return "\"Avoid\" - Eurogamer";
                else if (completion < 7f) return "\"Recommended\" - Eurogamer";
                else return "\"Essential\" - Eurogamer";
            case Critics.Giantbomb:
                string stars = "";
                score = Mathf.RoundToInt(completion * 5);

                for (int i = 0; i < 5; i++)
                {
                    if (score > 0)
                    {
                        stars += "★";
                        score--;
                    }
                    else
                    {
                        stars += "☆";
                    }
                }
                return stars + " - GameBomb";
            case Critics.Totalbiscuit:
                if (completion < 0.5f) return "\"No FOV slider, locked to 30FPS\" - TotalBiscuit";
                else if (completion < 0.7f) return "\"Not too shabby\" - TotalBiscuit";
                else return "\"Runs great on four GTX 1080 TIs\" - TotalBiscuit";
            case Critics.Steam:
                if (completion < 0.5f) return "\"[Not Recommended] Didn't even play it\" - a Steam Reviewer";
                else if (completion < 0.7f) return "\"[Recommended] Meh\" - a Steam Reviewer";
                else return "\"[Recommended] People who rate it negatively are idiots\" - a Steam Reviewer";
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
