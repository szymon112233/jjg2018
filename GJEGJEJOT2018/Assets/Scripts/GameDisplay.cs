using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
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
    }

    public void SetGameDisplay(string name, Color color, float programming, float music, float art, float testing)
    {
        Canvas.enabled = true;
        GameTitle.text = name;
        GameTitle.color = color;
        ProgrammingBar.value = programming;
        ArtBar.value = art;
        MusicBar.value = music;
        TestingBar.value = testing;
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
        // set task image
    }

    public void CloseGameDisplay()
    {
        Canvas.enabled = false;
    }
}
