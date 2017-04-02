using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	}


	#region Dialog

	//Casts a ray to see if we hit an NPC and, if so, we interact
	IEnumerator TryInteract (Collider col)
	{
		gameObject.GetComponent<VIDE_Assign> ().AssignNew ("Laura");
		//Lets grab the NPC's DialogueAssign script... if there's any
		VIDE_Assign assigned;
		assigned = this.gameObject.GetComponent<VIDE_Assign> ();
		//assign a dialogue ID
		int rndId = UnityEngine.Random.Range (0, GameObject.Find ("Quiz").GetComponent<SpawnPointController> ().convIDs.Count);
		assigned.overrideStartNode = GameObject.Find ("Quiz").GetComponent<SpawnPointController> ().convIDs [rndId];
		Debug.Log (assigned.overrideStartNode);
		//remove the used ID 
		GameObject.Find("Quiz").GetComponent<SpawnPointController>().convIDs.RemoveAt(rndId);

		if (!VIDE_Data.inScene) {
			Debug.LogError ("No VIDE_Data component in scene!");
			yield return null;

		}       

		if (!VIDE_Data.isLoaded) {
			//... and then use data to begin the conversation
			diagUI.Begin (assigned);
			GameController._playerState = PlayerState.Dialog;
			//set interface parts
			}

		while (!Input.GetKey (KeyCode.Space)) {
			yield return null;
		}
		//Hack - only works because we always only have one convo with these
		diagUI.CallNext ();
		yield return new WaitForEndOfFrame ();

		while (!Input.GetKey (KeyCode.Return)) {
			yield return null;
		}

		if (isActiveQuestion) {					
			diagUI.SaveAnswer ();
			isActiveQuestion = false;

			GameObject[] clones = GameObject.FindGameObjectsWithTag ("playerTexts");
			foreach (GameObject go in clones) {
				go.GetComponent<Text>().text = "";
			}

			diagUI.CallNext ();
		}

		diagUI.playerText.text = "";
		//Remove the question mark
		Destroy (gameObject);
	}
	

	void OnTriggerEnter (Collider col)
	{
		if (didCollide)
			return;
		
		if (col.tag.Equals ("Player")) {
			Debug.Log ("Player collided with question mark...!");

			StartCoroutine(TryInteract (col));
			isActiveQuestion = true;
			didCollide = true;
		}

	}

	#endregion
}
