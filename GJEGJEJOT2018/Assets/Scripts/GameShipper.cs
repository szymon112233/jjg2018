﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShipper : MonoBehaviour {
    public GameShippedDisplay GameShippedDisplay;
    private const float successProbabilityThreshold = .3f;
    private const float minimumSuccessRevenue = 1000f;

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if (obj.CompareTag(Game.tagName))
        {
            ShipGame(obj.GetComponent<Game>());
        }
    }

    private void ShipGame(Game game)
    {
        float successProbability = game.PercentFinished;
        float possibleRevenue = game.Revenue;

        if (successProbability < successProbabilityThreshold)
        {
            GameShippedDisplay.ShowFailMessage(game.Name, game.Color, game.PercentFinished);
            ThrowOut(game);
            return;
        }

        bool success = Random.Range(0f, 1f) <= successProbability;
        Debug.Log("SHIPPING: success " + success);
        float revenue = success ? successProbability * possibleRevenue : minimumSuccessRevenue;
        MoneyManager.I.AddMoney(revenue);

        if (success) GameShippedDisplay.ShowSuccessfulShipMessage(game.Name, game.Color, game.PercentFinished, revenue);
        else GameShippedDisplay.ShowUnsuccessfulShipMessage(game.Name, game.Color, game.PercentFinished, revenue);

        Destroy(game.gameObject);
    }

    private void ThrowOut(Game game)
    {
        Debug.Log("SHIPPING: game thrown out");
        Destroy(game.gameObject);
    }
}
