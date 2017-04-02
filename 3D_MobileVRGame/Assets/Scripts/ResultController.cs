using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResultController : MonoBehaviour {

	[HideInInspector]
	Dictionary<int, int > answers; // id, answer

	// Use this for initialization
	void Start () {
		answers = new Dictionary<int, int> ();
	}

	public void AddReply(int qID, int choice) {
		
		answers.Add (qID, choice);
		Debug.Log ("Added a new reply!");
		//EvaluateAnswers ();
	}
	/// <summary>
	/// Evaluates the answers.
	/// </summary>
	/// <returns>The answewrs.</returns>
	/// <param name="lifeGoal">Life goal. Can be: "rich" "family" "adventure")</param>
	public int EvaluateAnswers() {
		int amntRich = 0;
		int amntAdventure = 0;
		int amntFamily = 0;

	
		foreach (KeyValuePair<int, int> kvp in answers) {
			if (kvp.Value == 0) {
				amntRich++;
			} else if (kvp.Value == 1) {
				amntFamily++;
			} else if (kvp.Value == 2) {
				amntAdventure++;
			}
		}

		List<int> answers = new List<int>( new int[3] { amntRich, amntFamily, amntAdventure });
		answers.OrderByDescending (x => x);
	    Debug.Log ("Now in dict: " + answers.Count);
		return answers [0];
	}
}
