using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float MaxSpeed = 0.2f;
	public float ThrowSpeed = 2.0f;
    public GameDisplay GameDisplay;
	private CharacterController controller;
	private Game currGame;
	private SocketAttacher socketAttacher;
	private Animator animator;
	private bool move = true;

	// Use this for initialization
	void Start()
	{
		controller = GetComponent<CharacterController>();
		socketAttacher = GetComponent<SocketAttacher>();
		animator = GetComponentInChildren<Animator>();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		PickUpGame(hit);
		hit.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-hit.normal*1000,hit.point);
	}

	private void PickUpGame(ControllerColliderHit hit)
	{
		if (currGame != null)
			return;
		var body = hit.gameObject;
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
		var game	= DetachGame();
		if (game != null)
		{
			var rigid = game.GetComponent<Rigidbody>();
			if (rigid != null)
			{
				rigid.velocity = ThrowSpeed * transform.localToWorldMatrix.MultiplyVector(Vector3.forward + Vector3.down * 0.2f);
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
        GameDisplay.SetGameDisplay(currGame.Name,currGame.Color, currGame.Programming.percent, currGame.Music.percent, currGame.Art.percent, currGame.Testing.percent);
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
		controller.Move(moveVector * MaxSpeed * Time.deltaTime);
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
