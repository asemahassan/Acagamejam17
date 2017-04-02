using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResultController : MonoBehaviour
{

	[HideInInspector]
	Dictionary<int, int > _answers;
	// id, answer

	// Use this for initialization
	void Start ()
	{
		_answers = new Dictionary<int, int> ();
	}

	public void AddReply (int qID, int choice)
	{
		
		_answers.Add (qID, choice);
		Debug.Log ("Added a new reply!");

	}

	IEnumerator LoadSceneWithDelay (int sceneName, float delay)
	{
		yield return new WaitForSeconds (delay);
		SceneManager.LoadScene (sceneName);
	}

	public void GameEnding ()
	{
		EvaluateAnswers ();
		StartCoroutine (LoadSceneWithDelay (4, 1.0f));
	}

	/// <summary>
	/// Evaluates the answers.
	/// </summary>
	/// <returns>The answewrs.</returns>
	/// <param name="lifeGoal">Life goal. Can be: "rich" "family" "adventure")</param>
	public void EvaluateAnswers ()
	{
		int amntRich = 0;
		int amntAdventure = 0;
		int amntFamily = 0;

	
		foreach (KeyValuePair<int, int> kvp in _answers) {
			if (kvp.Value == 0) {
				amntRich++;
			} else if (kvp.Value == 1) {
				amntFamily++;
			} else if (kvp.Value == 2) {
				amntAdventure++;
			}
		}

		int cmp1 = Mathf.Max (amntRich, amntFamily);
		int cmp2 = Mathf.Max (amntAdventure, cmp1);


		if (cmp2 == amntRich) {
			PlayerPrefs.SetString ("Result", "rich");
		} else if (cmp2 == amntFamily) {
			PlayerPrefs.SetString ("Result", "family");
		} else
			PlayerPrefs.SetString ("Result", "adventure");

	
	}
}
