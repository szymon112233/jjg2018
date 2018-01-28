using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
	GameSpawner : MonoBehaviour
{
	public Transform GameBox;
	public List<Transform> trashProps = new List<Transform>();
	[Range(0,100)]public float TrashChance = 10;
	public float SpawnThrowSpeed = 20;
	public float AngularVelocityMax = 40;
	public float SpawnFrequency = 3;
	public float RushFrequency = 0.75f;
	private float ElapsedTime = 0;
	public int MinGames = 6;
	public int MaxGames = 10;
	private int CurrentGames;

	private void Start()
	{
		CurrentGames = GameObject.FindObjectsOfType<Game>().Length;
		var shipper = GameObject.FindObjectOfType<GameShipper>();
		if (shipper != null)
			shipper.Shipped.AddListener(OnGameShipped);
		var water = GameObject.FindObjectOfType<DestroyOnWater>();
		if (water != null)
			water.Destroyed.AddListener(OnGameShipped);
	}

	private void Update()
	{
		ElapsedTime += Time.deltaTime;
		if (ElapsedTime >= ((MinGames > CurrentGames) ? RushFrequency : SpawnFrequency))
		{
			ElapsedTime = 0;
			SpawnTask();
		}
	}

	public void SpawnTask()
	{
		if(trashProps.Count != 0)
		{
			if(Random.Range(CurrentGames < MinGames? 0.5f*TrashChance:0.0f,100.0f) < TrashChance)
			{
				var trash = Instantiate(trashProps[Random.Range(0,trashProps.Count)], transform.position, transform.rotation);
				var trashRB = trash.GetComponentInChildren<Rigidbody>();
				if (trashRB != null)
				{
					trashRB.velocity = SpawnThrowSpeed * transform.TransformVector(Vector3.forward);
					trashRB.angularVelocity = Random.insideUnitSphere * AngularVelocityMax;
				}
				return;
			}
		}
		if (MaxGames <= CurrentGames)
			return;
		var ret = Instantiate(GameBox, transform.position, transform.rotation);
		var rb = ret.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.velocity = SpawnThrowSpeed * transform.TransformVector(Vector3.forward);
			rb.angularVelocity = Random.insideUnitSphere * AngularVelocityMax;
		}
		CurrentGames++;
	}

	public void OnGameShipped()
	{
		CurrentGames--;
		if (CurrentGames == MaxGames - 1)
			ElapsedTime = 0;
	}
}
