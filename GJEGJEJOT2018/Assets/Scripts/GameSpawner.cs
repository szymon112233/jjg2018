using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    GameSpawner : MonoBehaviour
{
    public Transform GameBox;

    public void SpawnTask()
    {
        Instantiate(GameBox, transform.position, Quaternion.identity);
    }
}
