using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

	public DialogInterface diagUI;
	bool convoHasBegun;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
//	void Update () {
//		this.transform.LookAt(GameObject.Find("Player").transform);
//	}

	#region Dialog

	//Casts a ray to see if we hit an NPC and, if so, we interact
	void TryInteract(Collider col)
	{
		//        RaycastHit rHit;
		//
		//        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
		//        {
		//            //In this example, we will try to interact with any collider the raycast finds
		//
		//            //Lets grab the NPC's DialogueAssign script... if there's any
		VIDE_Assign assigned;
		//            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
		assigned = gameObject.GetComponent<VIDE_Assign>();
		//            else return;
		//               
		if (!VIDE_Data.inScene)
		{
			Debug.LogError("No VIDE_Data component in scene!");
			return;
		}       

		if (!VIDE_Data.isLoaded)
		{
			UnityEditor.EditorApplication.isPaused = true;
			//... and use it to begin the conversation
			diagUI.Begin(assigned);
			convoHasBegun = true;

			//set interface parts

		}
		else
		{


		}

		// }
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log("collision");
		TryInteract (col);

	}

	#endregion
}
