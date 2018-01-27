using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : EmployeeBar
{
    [SerializeField] private List<GameObject> icons;

    public override void ChangeColor(TaskEnum task)
    {
        base.ChangeColor(task);
        SetIcon(task);
    }

    public override void ChangeValue(float value)
    {
        base.ChangeValue(value);
        if (value == 0f)
        {
            DeactivateIcons();
        }
    }

    private void SetIcon(TaskEnum task)
    {
        DeactivateIcons();

        if ((int)task < icons.Count)
        {
            icons[(int)task].SetActive(true);
        }
    }

    private void DeactivateIcons()
    {
        foreach (GameObject obj in icons)
        {
            obj.SetActive(false);
        }
    }
}
