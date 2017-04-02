using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FacePlayer : MonoBehaviour
{
	public DialogInterface diagUI;
	private bool isActiveQuestion;
	private bool didCollide;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	//	 Update is called once per frame
	void Update ()
	{
		//this.transform.LookAt (GameObject.Find ("Player").transform);

		if (GameController._playerState == PlayerState.Dialog) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				if (VIDE_Data.isLoaded)
				//If conversation already began, let's just progress through it
				diagUI.CallNext ();
			}

			if (Input.GetKeyDown (KeyCode.Return)) {
				if (isActiveQuestion) {					
					diagUI.SaveAnswer ();
					isActiveQuestion = false;
				}
			}
		}
	}

	#region Dialog

	//Casts a ray to see if we hit an NPC and, if so, we interact
	void TryInteract (Collider col)
	{
		gameObject.GetComponent<VIDE_Assign> ().AssignNew ("Laura");
		//Lets grab the NPC's DialogueAssign script... if there's any
		VIDE_Assign assigned;
		assigned = this.gameObject.GetComponent<VIDE_Assign> ();
		//assign a dialogue ID
		int rndId = UnityEngine.Random.Range (0, GameObject.Find ("Quiz").GetComponent<SpawnPointController> ().convIDs.Count);
		assigned.assignedID = GameObject.Find ("Quiz").GetComponent<SpawnPointController> ().convIDs [rndId];
		//remove the used ID 
		GameObject.Find("Quiz").GetComponent<SpawnPointController>().convIDs.RemoveAt(rndId);

		if (!VIDE_Data.inScene) {
			Debug.LogError ("No VIDE_Data component in scene!");
			return;
		}       

		if (!VIDE_Data.isLoaded) {
			//... and then use data to begin the conversation
			diagUI.Begin (assigned);
			GameController._playerState = PlayerState.Dialog;
			//set interface parts

		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (didCollide)
			return;
		
		if (col.tag.Equals ("Player")) {
			Debug.Log ("Player collided with question mark...!");

			TryInteract (col);
			isActiveQuestion = true;
			didCollide = true;
		}

	}

	#endregion
}
