using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
	[HideInInspector] public bool IsInRange { get; private set; }
	[HideInInspector] public GameObject DetectedGameObject { get; private set; }

	#region Collider managment

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
	}

	#endregion

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			IsInRange = state;
		}
	}
}
