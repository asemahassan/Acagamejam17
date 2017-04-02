using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beggar : MonoBehaviour
{
	PlayerController playerCtrl = null;
	bool hasHelped = false;
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
		if (hasHelped) {
			//do 3 times action to get meat loaf from animal as food
			if (playerCtrl.moneyGiven == 4) {
				hasHelped = false;

				GameController.UpdateQuestItemsCount (QuestType.Beggar);
				GameController._playerState = PlayerState.Idle;
				//Kill animal --hide destroy object
				Destroy (this.gameObject, 1.0f);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {

			hasHelped = true;
		}
	}
}
