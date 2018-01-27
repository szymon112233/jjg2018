using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float MaxSpeed = 0.2f;
	public float ThrowSpeed = 2.0f;
	private CharacterController controller;
	private GameObject gameSocket;
	private GameObject dropSocket;
	// Use this for initialization
	void Start()
	{
		controller = GetComponent<CharacterController>();
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

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		var body = hit.gameObject;
		if (body.tag != "Game Box")
			return;
		GameObject child = DetachGame();
		if (child != null)
		{
			child.transform.position = dropSocket.transform.position;
		}
		Rigidbody rigid = body.GetComponent<Rigidbody>();
		rigid.useGravity = false;
		rigid.detectCollisions = false;
		body.transform.SetParent(gameSocket.transform);
		body.transform.localRotation = Quaternion.identity;
		rigid.velocity = rigid.angularVelocity = body.transform.localPosition = Vector3.zero;
		body.GetComponent<FancyRotator>().enabled = true;
	}

	private GameObject DetachGame()
	{
		if (gameSocket.transform.childCount == 0)
			return null;
		var child = gameSocket.transform.GetChild(0).gameObject;
		child.GetComponent<FancyRotator>().enabled = false;
		var rigid = child.GetComponent<Rigidbody>();
		rigid.detectCollisions = true;
		rigid.useGravity = true;
		gameSocket.transform.DetachChildren();
		return child;
	}

	// Update is called once per frame
	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 moveVector = new Vector3(x, 0, y);
		controller.Move(moveVector * MaxSpeed * Time.deltaTime);
		if (moveVector.magnitude > 0)
		{
			transform.LookAt(transform.position + moveVector);
		}
		if (Input.GetAxis("Fire1") > 0)
		{
			var game = DetachGame();
			if(game != null)
			{
				var rigid = game.GetComponent<Rigidbody>();
				if(rigid != null)
				{
					rigid.velocity = ThrowSpeed * transform.localToWorldMatrix.MultiplyVector(Vector3.forward);
				}
			}
		}
	}
}
