using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunting : MonoBehaviour
{
	PlayerController playerCtrl = null;
	bool isHunting = false;
	// Use this for initialization
	void Start ()
	{
		if (playerCtrl == null) {
			playerCtrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isHunting)
			//do 3 times action to get meat loaf from animal as food
		if (playerCtrl.huntDown == 3) {
			//Kill animal --hide destroy object
			Destroy (this.gameObject, 1.0f);
			isHunting = false;
			GameController.UpdateQuestItemsCount (QuestType.Food);
			GameController._playerState = PlayerState.Idle;
			playerCtrl.huntDown = 0;
			playerCtrl.HideSword ();
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {

			//take out sword
			GameController._playerState = PlayerState.Hunt;
			Debug.Log ("Start hunting");
			isHunting = true;
		}
		
	}
}
