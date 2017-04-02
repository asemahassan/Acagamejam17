using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public int EvaluateAnswers(string lifeGoal) {
		int amntRich = 0;
		int amntAdventure = 0;
		int amntFamily = 0;

		switch (lifeGoal) {
		case "rich":
			foreach (KeyValuePair<int, int> kvp in answers) {
				if (kvp.Value == 0)
					amntRich++;
			}
			return amntRich;
					

		case "family": 
			foreach (KeyValuePair<int, int> kvp in answers) {
				if (kvp.Value == 1)
					amntFamily++;
			}
			return amntFamily++;

		case "adventure":
			foreach (KeyValuePair<int, int> kvp in answers) {
				if (kvp.Value == 2)
					amntAdventure++;
			}
			return amntAdventure;
		default:
			
			Debug.LogError ("You passed an unknown request string. Maybe check your spelling?");
			return -1;

		}
		//go through answers, raise numbers, compare, return mostgivenanswer


		Debug.Log ("Now in dict: " + answers.Count);
	}
}
