﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    GameSpawner : MonoBehaviour
{
    public Transform GameBox;
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
	}

	private void Update()
	{
		ElapsedTime += Time.deltaTime;
		if(MaxGames > CurrentGames && ElapsedTime >= ((MinGames > CurrentGames)?RushFrequency:SpawnFrequency))
		{
			ElapsedTime = 0;
			SpawnTask();
		}
	}

	public void SpawnTask()
    {
        var ret = Instantiate(GameBox, transform.position, transform.rotation);
		var rb = ret.GetComponent<Rigidbody>();
		rb.velocity = SpawnThrowSpeed * transform.TransformVector(Vector3.forward);
		rb.angularVelocity = Random.insideUnitSphere*AngularVelocityMax;
		CurrentGames++;
    }

	private void OnGameShipped()
	{
		CurrentGames--;
	}
}
