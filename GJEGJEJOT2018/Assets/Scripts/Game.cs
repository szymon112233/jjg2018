﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const string tagName = "Game Box";

    public string Name = "Generic Game";

    private Task Programming;
    private Task Music;
    private Task Art;
    private Task Testing;

    private Task currTask;
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
    }

    public void WorkOn(float speed)
    {
        currTask.LowerPercent(speed);
    }

    public void SwitchTask()
    {
        currTaskInfo = (TaskEnum)(((int)currTaskInfo + 1) % ((int)TaskEnum.TESTING + 1));
        currTask = GetTask(currTaskInfo);
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
