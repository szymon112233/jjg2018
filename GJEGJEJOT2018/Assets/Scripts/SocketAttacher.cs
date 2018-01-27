using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketAttacher : MonoBehaviour
{

	private GameObject gameSocket;
	private GameObject dropSocket;
	// Use this for initialization
	void Start()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject.tag == "GameSocket")
			{
				gameSocket = child.gameObject;
			}
			if (child.gameObject.tag == "DropSocket")
			{
				dropSocket = child.gameObject;
			}
		}
	}

	public void MoveToDropSocket(Game game)
	{
		if (dropSocket != null)
			game.transform.position = dropSocket.transform.position;
	}

	public void DetachFromGameSocket(Game game)
	{
		if (gameSocket != null)
		{
			game.StartPhysics();
			game.transform.SetParent(null);
		}
	}

	public void AttachToGameSocket(Game game, bool useFancyRotator = true)
	{
		if (gameSocket != null)
		{
			game.transform.SetParent(gameSocket.transform);
			game.transform.position = Vector3.zero;
			game.StopPhysics(useFancyRotator);
		}
	}
}
