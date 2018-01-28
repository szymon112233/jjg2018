using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyOnWater : MonoBehaviour {

	public UnityEvent Destroyed;
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Game Box"))
		{
			Destroy(collision.gameObject);
			Destroyed.Invoke();
		}
    }

}
