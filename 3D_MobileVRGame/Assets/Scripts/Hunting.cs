using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunting : MonoBehaviour
{
	PlayerController playerCtrl = null;
	bool isHunting = false;

	[SerializeField]
	GameObject meatPieces = null;
	//	[SerializeField]
	//	AudioSource _audio = null;

	// Use this for initialization
	void Start ()
	{
//		if (_audio == null) {
//			_audio = this.GetComponent<AudioSource> ();
//		}
		if (playerCtrl == null) {
			playerCtrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isHunting)
			//do 3 times action to get meat loaf from animal as food
		if (playerCtrl.huntDown == 4) {
			isHunting = false;
			for (int k = 0; k <= 3; k++) {
				GameController.CreateMeatObject (this.transform.position);
			}
			GameController.UpdateQuestItemsCount (QuestType.Hunt);

			GameController.UpdatePromptMessages ("Pick meat pieces; food stock");
			GameController._playerState = PlayerState.Idle;
			playerCtrl.huntDown = 0;
			playerCtrl.HideSword ();

			//Kill animal --hide destroy object
			Destroy (this.gameObject, 1.0f);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {
			//take out sword
			GameController.UpdatePromptMessages ("Use 'X' Key to Hunt down this animal");

			PlayerController._huntingAnimalPos = this.transform.position;
			GameController._playerState = PlayerState.Hunt;
			Debug.Log ("Start hunting");
			isHunting = true;
		}
	}
}
