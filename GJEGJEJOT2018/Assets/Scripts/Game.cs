using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public const string tagName = "Game Box";

	public string Name = "Generic Game";

	public Task Programming;
    public Task Music;
    public Task Art;
    public Task Testing;

    public Color Color;

	private Task currTask;
	private Rigidbody rigidBody;
	private TaskEnum currTaskInfo = TaskEnum.PROGRAMMING;

	public TaskEnum CurrTask
	{
		get { return currTaskInfo; }
	}

	private void Awake()
	{
		Build();
	}

	private void Build()
	{
		int percentLeft = 100;

		int nextpercent = Random.Range(0, percentLeft + 1);
		Programming = new Task(nextpercent);
		percentLeft -= nextpercent;

		nextpercent = Random.Range(0, percentLeft + 1);
		Testing = new Task(nextpercent);
		percentLeft -= nextpercent;

		nextpercent = Random.Range(0, percentLeft + 1);
		Art = new Task(nextpercent);
		percentLeft -= nextpercent;

		nextpercent = percentLeft;
		Music = new Task(nextpercent);

		currTask = Programming;

		rigidBody = GetComponent<Rigidbody>();

        Color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        gameObject.GetComponent<Renderer>().material.color = Color;
        
	}

	public bool WorkOn(float speed, EmployeeBar progressBar)
	{
		currTask.LowerPercent(speed);
        progressBar.ChangeColor(CurrTask);
        progressBar.ChangeValue(currTask.percent);

        return currTask.IsEnded();
	}

	public void SwitchTask()
	{
		currTaskInfo = (TaskEnum)(((int)currTaskInfo + 1) % ((int)TaskEnum.TESTING + 1));
		currTask = GetTask(currTaskInfo);
		Debug.Log("SWITCHING TASK TO: " + currTaskInfo);
	}

	private Task GetTask(TaskEnum task)
	{
		switch (task)
		{
			case TaskEnum.ART:
				return this.Art;
			case TaskEnum.MUSIC:
				return this.Music;
			case TaskEnum.PROGRAMMING:
				return this.Programming;
			case TaskEnum.TESTING:
				return this.Testing;
			default:
				return new Task(0);
		}
	}

	public void StopPhysics(bool useFancyRotator)
	{
		rigidBody.useGravity = false;
		rigidBody.detectCollisions = false;
		transform.localRotation = Quaternion.identity;
		rigidBody.velocity = rigidBody.angularVelocity = transform.localPosition = Vector3.zero;
		GetComponent<FancyRotator>().enabled = useFancyRotator;
	}

	public void StartPhysics()
	{
		GetComponent<FancyRotator>().enabled = false;
		rigidBody.detectCollisions = true;
		rigidBody.useGravity = true;
	}

}
