using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour {
	public Button start;
	// Use this for initialization
	void Start () {
		start.Select();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Exit()
    {
        Application.Quit();
    }
}
