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
    public Image TaskImage;

    public void SetGameDisplay(string name, Color color, float programming, float music, float art, float testing)
    {
        GameTitle.text = name;
        GameTitle.color = color;
        ProgrammingBar.value = programming;
        ArtBar.value = art;
        MusicBar.value = music;
        TestingBar.value = testing;
    }

    public void SetTask(TaskEnum currentTask)
    {
        // set task image
    }
}
