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
        string criticsMessage = RandomizeScoreText(completion);
        GameTitle.text = title + " has been shipped!";
        GameScore.text = "Critics say: " + criticsMessage;
        EarnedMoney.text = "We made " + earnedMoney + " bucks!";


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

    //private IEnumerator ShowDisplay()
    //{

    //}
}
