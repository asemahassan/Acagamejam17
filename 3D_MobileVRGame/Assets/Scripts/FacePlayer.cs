using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
	public DialogInterface diagUI;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	//	 Update is called once per frame
	void Update ()
	{
		//this.transform.LookAt (GameObject.Find ("Player").transform);

		if (GameController._playerState == PlayerState.Dialog) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				//If conversation already began, let's just progress through it
				diagUI.CallNext ();
			}

			if (Input.GetKeyDown (KeyCode.Return)) {
				diagUI.SaveAnswer ();

			}
		}
	}

	#region Dialog

	//Casts a ray to see if we hit an NPC and, if so, we interact
	void TryInteract (Collider col)
	{
		//Lets grab the NPC's DialogueAssign script... if there's any
		VIDE_Assign assigned;
		assigned = this.gameObject.GetComponent<VIDE_Assign> ();

		if (!VIDE_Data.inScene) {
			Debug.LogError ("No VIDE_Data component in scene!");
			return;
		}       

		if (!VIDE_Data.isLoaded) {
			//... and use it to begin the conversation
			diagUI.Begin (assigned);
			GameController._playerState = PlayerState.Dialog;
			//set interface parts

		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {
			Debug.Log ("Player collided with question mark...!");
			TryInteract (col);
		}

	}

	#endregion
}
