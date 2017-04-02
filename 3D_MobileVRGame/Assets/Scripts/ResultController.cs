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

	public void AddReply(Dictionary<int, int> reply) {
		//int id = reply.
		//answers.Ad
		Debug.Log ("Added a new reply!");
	}

	public string EvaluateAnswers() {
		int amntRich = 0;
		int amntAdventure = 0;
		int amntFamily = 0;

		//go through answers, raise numbers, compare, return mostgivenanswer
		string answer = "";
		return answer;
	}
}
