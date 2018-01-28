using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraInterpolater : MonoBehaviour
{
	Transform startPos;
	Transform endPos;
	new Camera camera;
	private MoneyManager money;
	private Controller player;
	private GameSpawner spawner;
	private Canvas canvas;
	public float InterpTime = 5f;
	private float elaspedTime = 0;
	bool play = false;
	// Use this for initialization
	void Start()
	{
		startPos = GameObject.FindGameObjectWithTag("CameraStartPos").transform;
		endPos = GameObject.FindGameObjectWithTag("CameraEndPos").transform;
		camera = GetComponent<Camera>();

		money = GameObject.FindObjectOfType<MoneyManager>();
		player = GameObject.FindObjectOfType<Controller>();
		spawner = GameObject.FindObjectOfType<GameSpawner>();
		canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
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
		if (!play && !Input.GetButtonDown("Fire3"))
			return;
		BeginPlay();

		elaspedTime += Time.deltaTime / InterpTime;

		float effectiveTime = elaspedTime - 0.1f;
		camera.transform.position = Vector3.Lerp(startPos.position, endPos.position, effectiveTime);
		camera.transform.rotation = Quaternion.Slerp(startPos.rotation, endPos.rotation, effectiveTime * 3f);

		if (effectiveTime > 1.2f)
		{
			StartGsme();
		}
	}
}
