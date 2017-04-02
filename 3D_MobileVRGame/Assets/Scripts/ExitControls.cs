using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitControls : MonoBehaviour
{

	ResultController resultCtrl = null;
	// Use this for initialization
	void Start ()
	{
		if (resultCtrl == null) {
			resultCtrl = GameObject.FindGameObjectWithTag ("Result").GetComponent<ResultController> ();
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{

		if (col.tag.Equals ("Player")) {
			if (this.gameObject.transform.parent.name.Equals ("Phase_1_Exit")) {
				GameController.UpdatePromptMessages ("Phase 2 is locked\n" +
				"Complete Quests and answer Questions to unlock");

			} else if (this.gameObject.transform.name.Equals ("Phase_2_Exit")) {
				GameController.UpdatePromptMessages ("Phase 3 is locked\n" +
				"Complete Quests and answer Questions to unlock");

			} else if (this.gameObject.transform.name.Equals ("Phase_3_Exit")) {
				GameController.UpdatePromptMessages ("Congratulations ! You have reached end of game");

				resultCtrl.GameEnding ();
			}

		}
	}

	void OnTriggerExit (Collider col)
	{

		if (col.tag.Equals ("Player")) {
			if (this.gameObject.transform.parent.name.Equals ("Phase_1_Exit")) {
				GameController.UpdatePromptMessages ("Find items to interact with");

			} else if (this.gameObject.transform.name.Equals ("Phase_2_Exit")) {
				GameController.UpdatePromptMessages ("Find items to interact with");

			} else if (this.gameObject.transform.name.Equals ("Phase_3_Exit")) {
				GameController.UpdatePromptMessages ("Congratulations ! You have reached end of game");

				//Call Sebastian Result evaluation methods here
			}

		}
	}
}
