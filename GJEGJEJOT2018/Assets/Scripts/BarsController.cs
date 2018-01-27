using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour {

    public RectTransform targetCanvas;
    public RectTransform thisRect;
    public Transform objectToFollow;
    public List<EmployeeBar> bars;

    public ProgressBar progressBar;

    void Update()
    {
        RepositionBar();
    }

    private void RepositionBar()
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(objectToFollow.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));
        //now you can set the position of the ui element
        thisRect.anchoredPosition = WorldObject_ScreenPosition;
    }

    public EmployeeBar GetBar(TaskEnum task)
    {
        if (bars.Count > (int)task)
            return bars[(int)task];

        return null;
    }
}
