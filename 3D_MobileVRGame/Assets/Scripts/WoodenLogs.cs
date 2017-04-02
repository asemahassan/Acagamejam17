using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLogs : MonoBehaviour
{
	PlayerController playerCtrl = null;
	// Use this for initialization
	void Start ()
	{
		if (playerCtrl == null) {
			playerCtrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {

			GameController.UpdateQuestItemsCount (QuestType.Woods);
			Destroy (this.gameObject, 1.0f);
		}

	}
}
