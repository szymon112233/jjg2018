using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyRotator : MonoBehaviour
{
	public float PositionAmplitude = 0.5f;
	public float AngularAmplitude = 3f;
	private float elapsedTime = 0.0f;
	public float PositionPeriod = 13.0f/5.0f;
	public float AngularVelocityPeriod = 17.0f/3.0f;
	private Vector3 startLocation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		transform.localPosition = startLocation + Vector3.up * PositionAmplitude * (1 + Mathf.Sin(elapsedTime*(Mathf.PI *2) / PositionPeriod));
		transform.Rotate(0,AngularAmplitude* Mathf.Sin(elapsedTime / AngularVelocityPeriod *(Mathf.PI *2)), 0);
	}

	private void OnEnable()
	{
		elapsedTime = 0;
		startLocation = transform.localPosition;
		var rigid = GetComponent<Rigidbody>();
		if(rigid != null)
		{
			rigid.useGravity = false;
			rigid.detectCollisions = false;
			rigid.velocity = rigid.angularVelocity = Vector3.zero;
		}
	}

	private void OnDisable()
	{
		transform.localPosition = startLocation;
		var rigid = GetComponent<Rigidbody>();
		if (rigid != null)
		{
			rigid.detectCollisions = true;
			rigid.useGravity = true;
		}
	}
}
