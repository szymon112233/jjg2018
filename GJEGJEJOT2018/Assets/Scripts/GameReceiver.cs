using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReceiver : MonoBehaviour
{
    public string BoxTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(BoxTag))
        {
            Game game = GetComponent<Game>();
        }
    }

}
