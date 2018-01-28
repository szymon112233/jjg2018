using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraInterpolater : MonoBehaviour
{
	Transform startPos;
	Transform endPos;
	private Transform creditPos;
	new Camera camera;
	private MoneyManager money;
	private Controller player;
	private GameSpawner spawner;
	private Canvas canvas;
	private MenuCanvas menu;
	public float InterpTime = 5f;
	private float elaspedTime = 0;
	bool play = false;
	bool credits = false;
	bool backCredits = false;


	// Use this for initialization
	void Start()
	{
		startPos = GameObject.FindGameObjectWithTag("CameraStartPos").transform;
		endPos = GameObject.FindGameObjectWithTag("CameraEndPos").transform;
		creditPos = GameObject.FindGameObjectWithTag("CameraCreditsPos").transform;
		camera = GetComponent<Camera>();

		money = GameObject.FindObjectOfType<MoneyManager>();
		player = GameObject.FindObjectOfType<Controller>();
		spawner = GameObject.FindObjectOfType<GameSpawner>();
		canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
		menu = GameObject.FindObjectOfType<MenuCanvas>();
	}

	public void ShowCredits()
	{
		elaspedTime = 0;
		credits = true;
		menu.Hide();
	}

	public void BeginPlay() //#UE4
	{
		play = true;
	}

	void StartGsme()
	{
		player.enabled = true;
		spawner.enabled = true;
		canvas.enabled = true;
		money.enabled = true;
		Destroy(this);
	}

	// Update is called once per frame
	void Update()
	{
		if (backCredits)
		{
			elaspedTime += Time.deltaTime / InterpTime;

			float effectiveTime = elaspedTime - 0.1f;
			camera.transform.position = Vector3.Lerp(creditPos.position, startPos.position, effectiveTime);
			camera.transform.rotation = Quaternion.Slerp(creditPos.rotation, startPos.rotation, effectiveTime);

			if (effectiveTime > 1.10f)
			{
				backCredits = false;
				elaspedTime = 0;
				menu.Show();
			}
			return;
		}

		if (credits)
		{
			if (elaspedTime - 0.1f > 1.10f && Input.GetButtonDown("Cancel"))
			{
				ShowReturnButton();
				return;
			}
			elaspedTime += Time.deltaTime / InterpTime;

			float effectiveTime = elaspedTime - 0.1f;
			camera.transform.position = Vector3.Lerp(startPos.position, creditPos.position, effectiveTime);
			camera.transform.rotation = Quaternion.Slerp(startPos.rotation, creditPos.rotation, effectiveTime);

			return;
		}
		if (!play)
			return;

		{
			elaspedTime += Time.deltaTime / InterpTime;

			float effectiveTime = elaspedTime - 0.1f;
			camera.transform.position = Vector3.Lerp(startPos.position, endPos.position, effectiveTime);
			camera.transform.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, effectiveTime * 3f);

			if (effectiveTime > 1.10f)
			{
				StartGsme();
			}
		}
	}

	private void ShowReturnButton()
	{
		credits = false;
		elaspedTime = 0;
		ReturnToMainMenuFromCredits();
	}

	private void ReturnToMainMenuFromCredits()
	{
		backCredits = true;
	}
}
