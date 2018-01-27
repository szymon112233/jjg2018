using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
    public const string tagName = "MainCanvas";

    public Text GameTitle;
    public Slider ProgrammingBar;
    public Slider ArtBar;
    public Slider MusicBar;
    public Slider TestingBar;
    //public Image TaskImage;
    public Text TaskText;
    public Canvas Canvas;

    private void Awake()
    {
        Canvas.enabled = false;

        SetBarColor(ProgrammingBar, Task.GetColor(TaskEnum.PROGRAMMING));
        SetBarColor(ArtBar, Task.GetColor(TaskEnum.ART));
        SetBarColor(MusicBar, Task.GetColor(TaskEnum.MUSIC));
        SetBarColor(TestingBar, Task.GetColor(TaskEnum.TESTING));
    }

    public void SetGameDisplay(string name, Color color, float programming, float music, float art, float testing)
    {
        Canvas.enabled = true;
        GameTitle.text = name;
        GameTitle.color = color;
        SetBarFill(ProgrammingBar, programming);
        SetBarFill(ArtBar, art);
        SetBarFill(MusicBar, music);
        SetBarFill(TestingBar, testing);
    }

    public void SetTask(TaskEnum currentTask)
    {
        switch(currentTask)
        {
            case TaskEnum.PROGRAMMING:
                TaskText.text = "Programming";
                break;
            case TaskEnum.ART:
                TaskText.text = "Art";
                break;
            case TaskEnum.MUSIC:
                TaskText.text = "Music";
                break;
            case TaskEnum.TESTING:
                TaskText.text = "Testing";
                break;
            default:
                break;
        }

        TaskText.color = Task.GetColor(currentTask);
        // set task image
    }

    public void CloseGameDisplay()
    {
        Canvas.enabled = false;
    }

    private void SetBarFill(Slider bar, float fill)
    {
        if (fill == 0f) bar.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>().enabled = false;
        else
        {
            bar.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>().enabled = true;
            bar.value = fill;
        }
    }

    private void SetBarColor(Slider slider, Color color)
    {
        slider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>().color = color;
    }
}
