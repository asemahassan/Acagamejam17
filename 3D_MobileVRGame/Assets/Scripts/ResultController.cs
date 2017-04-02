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
		EvaluateAnswers ();
	}

	public string EvaluateAnswers() {
		int amntRich = 0;
		int amntAdventure = 0;
		int amntFamily = 0;

		//go through answers, raise numbers, compare, return mostgivenanswer
		string answer = "";
		return answer;

		Debug.Log ("Now in dict: " + answers.Count);
	}
}
