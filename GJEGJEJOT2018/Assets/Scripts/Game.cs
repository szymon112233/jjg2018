using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public const string tagName = "Game Box";
    private const float minRevenue = 10000f;
    private const float maxRevenue = 20000f;

	public string Name = "Generic Game";

	public Task Programming;
    public Task Music;
    public Task Art;
    public Task Testing;
    public Color Color;

	private Task currTask;
	private Rigidbody rigidBody;

	private TaskEnum currTaskInfo = TaskEnum.PROGRAMMING;
    private float revenue = 0f;

	public TaskEnum CurrTask
	{
		get { return currTaskInfo; }
	}

    public float PercentFinished
    {
        get
        {
            return 1 -
                (
                    Programming.percent +
                    Music.percent +
                    Art.percent +
                    Testing.percent
                );
        }
    }

    public float Revenue
    {
        get { return revenue; }
    }

	private void Awake()
	{
		Build();
	}

	private void Build()
	{
		int programmingWeight = Random.Range(1, 100),
			testingWeight = Random.Range(1, 100),
			artWeight = Random.Range(1, 100),
			musicWeight = Random.Range(1, 100);
		float sum = programmingWeight+testingWeight + artWeight+musicWeight;

		int musicDiff = (int)(100*(musicWeight / sum)),
			artDiff = (int)(100 * (artWeight / sum)),
			testDiff = (int)(100 * (testingWeight / sum)),
			progDiff = 100 - musicDiff - testDiff-artDiff;

		Programming = new Task(progDiff);
		Testing = new Task(testDiff);
		Art = new Task(artDiff);
		Music = new Task(musicDiff);

		currTask = Programming;

		rigidBody = GetComponent<Rigidbody>();


        revenue = Random.Range(minRevenue, maxRevenue);
		
		var material = gameObject.GetComponent<Renderer>().material;
        Color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        material.SetColor("_Color", Color);
		material.SetColor("_OutlineColor", Task.GetColor(currTaskInfo));	
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
		gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Task.GetColor(currTaskInfo));
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
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;
		rigidBody.detectCollisions = false;
		transform.localRotation = Quaternion.identity;
		rigidBody.velocity = rigidBody.angularVelocity = transform.localPosition = Vector3.zero;
		GetComponent<FancyRotator>().enabled = useFancyRotator;
	}

	public void StartPhysics()
	{
		rigidBody.isKinematic = false;
		GetComponent<FancyRotator>().enabled = false;
		rigidBody.detectCollisions = true;
		rigidBody.useGravity = true;
	}
}
