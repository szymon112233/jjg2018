using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public string Name = "Generic Game";

    private Task Programming;
    private Task Music;
    private Task Art;
    private Task Testing;

    private Task currTask;

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
    }

    public void WorkOn(TaskEnum task, float speed)
    {
        currTask = GetTask(task);
        currTask.LowerPercent(speed);
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

}
