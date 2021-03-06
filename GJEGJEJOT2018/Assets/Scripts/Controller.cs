﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float MaxSpeed = 0.2f;
	public float ThrowSpeed = 2.0f;
	public GameDisplay GameDisplay;
	private Game currGame;
	private SocketAttacher socketAttacher;
	private Animator animator;
	private bool move = true;
    private AudioSource throwAudio;

	// Use this for initialization
	void Start()
	{
		socketAttacher = GetComponent<SocketAttacher>();
		animator = GetComponentInChildren<Animator>();
        throwAudio = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		PickUpGame(collision);
		//var rb = hit.gameObject.GetComponent<Rigidbody>();
		//if (rb != null)
		//	rb.AddForceAtPosition(-hit.normal * 1000, hit.point);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
	}

	private void PickUpGame(Collision collision)
	{
		if (currGame != null)
			return;
		var body = collision.gameObject;
		if (!body.CompareTag(Game.tagName))
			return;
		var game = DetachGame();
		if (game != null)
			socketAttacher.MoveToDropSocket(game);
		currGame = body.GetComponent<Game>();
		AttachGame();
	}

	private void OnThrowApex()
	{
		var game = DetachGame();
        throwAudio.Play();
		if (game != null)
		{
			var rigid = game.GetComponent<Rigidbody>();
			if (rigid != null)
			{
				rigid.velocity = ThrowSpeed * transform.localToWorldMatrix.MultiplyVector(Vector3.forward + Vector3.up * 0.2f);
				rigid.angularVelocity = Random.insideUnitSphere * 15;
			}
		}
	}

	private void OnEnableMovement()
	{
		move = true;
	}

	private Game DetachGame()
	{
		if (currGame == null)
			return null;
		animator.SetBool("HasBox", false);
		socketAttacher.DetachFromGameSocket(currGame);
		var tmp = currGame;
		currGame = null;
		GameDisplay.CloseGameDisplay();
		Debug.Log(tmp.name);
		return tmp;
	}

	private void AttachGame()
	{
		if (currGame == null)
			return;
		socketAttacher.AttachToGameSocket(currGame, false);
		animator.SetBool("HasBox", true);
		animator.ResetTrigger("Throw");
		GameDisplay.SetGameDisplay(currGame.Name, currGame.Color, currGame.Programming.percent, currGame.Music.percent, currGame.Art.percent, currGame.Testing.percent);
		GameDisplay.SetTask(currGame.CurrTask);
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		DispenseTask();

		CheckSwitchTaskInput();
	}

	private void DispenseTask()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if (currGame != null)
			{
				animator.SetTrigger("Throw");
				move = false;
			}
		}
	}

	private void Move()
	{
		if (!move)
			return;
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 moveVector = new Vector3(x, 0, y);
		transform.position += (moveVector * MaxSpeed * Time.deltaTime);
		animator.SetBool("IsRunning", moveVector.magnitude > 0);
		if (moveVector.magnitude > 0)
		{
			transform.LookAt(transform.position + moveVector);
		}
	}

	private void CheckSwitchTaskInput()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			if (currGame != null)
			{
				currGame.SwitchTask();
				GameDisplay.SetTask(currGame.CurrTask);
			}
		}
	}
}
