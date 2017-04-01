using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Freeman : MonoBehaviour
{
	string[] WordsGuessList = { "jockey", "finish", "belief", "dagger", "jammer" };
	string originalWord;
	string showWord;
	int[] hideindex = new int[]{ 0, 0, 0, 0, 0 };
	int currentindex = 0;
	int mistakecounter = 0;

	//Hide this game canvas
	[SerializeField]
	GameObject freemanRef = null;
	//place holders text
	[SerializeField]
	List<InputField> inputFieldsList = new List<InputField> ();

	[SerializeField]
	List<Image> hangManBody = new List<Image> ();

	[SerializeField]
	Text promptMsg = null;
	InputField currentField = null;
	InputField prevInputField = null;

	// Use this for initialization
	void Start ()
	{
		
	}

	void OnEnable ()
	{
		mistakecounter = 0;
		currentindex = 0;
		hideindex = new int[]{ 0, 0, 0, 0, 0 };
		originalWord = "";
		showWord = "";

		prevInputField = null;
		currentField = null;

		//Resume player state
		GameController._playerState = PlayerState.None;
		PickARandomWord ();
		OrganiseWord ();
		ShowPromptMessage ("");
	}

	void OnDisable ()
	{
		//Resume player state
		GameController._playerState = PlayerState.Idle;
	}

	void ShowPromptMessage (string msg)
	{

		if (promptMsg != null) {
			promptMsg.text = msg;
		}
	}
	// Invoked when the value of the text field changes.
	public void ValueChangeCheck ()
	{
		GameObject activeObj = EventSystem.current.currentSelectedGameObject;
		if (activeObj != null) {
			currentField = activeObj.GetComponent<InputField> ();
//			if (_currentField.text.Length > 0 && _currentField.text.Length <= 1) {
//				Debug.Log ("Value Changed: " + _currentField.text);
//			}
		}
		currentField.onEndEdit.AddListener (delegate {
			LockInput ();
		});
	}

	// Checks if there is anything entered into the input field.
	void LockInput ()
	{
		if (currentField.text.Length > 0 && currentField.text.Length <= 1) {
			//only pass the character
			EvaluateUserInput (currentField.text [0]);
		} else if (currentField.text.Length == 0) {
			Debug.Log ("Main Input Empty");
		}
	}

	void PickARandomWord ()
	{
		if (WordsGuessList.Length > 0) {
			int randIndex = UnityEngine.Random.Range (0, WordsGuessList.Length);
			originalWord = WordsGuessList [randIndex];
		}
	}
	//To check unique values in the array for hide indexes
	bool hasValue (int value)
	{
		if (hideindex.Length > 0) {
			for (int k = 0; k < hideindex.Length; k++) {
				if (hideindex [k] == value) {
					return true;
				}
			}
		}		
		return  false;
	
	}

	string ReplaceCharacterAtIndex (string refString, int index, string value)
	{
		refString = refString.Remove (index, 1);
		refString = refString.Insert (index, value);

		return refString;
	}

	void OrganiseWord ()
	{
		showWord = originalWord;
		Debug.Log ("Original word:" + showWord);

		for (int i = 0; i < hideindex.Length; i++) {
			int index = UnityEngine.Random.Range (0, originalWord.Length);
			//to check if the value exist already or not
			while (hasValue (index)) {
				index = UnityEngine.Random.Range (0, originalWord.Length);
			}
			//if its  UNIQUE value only then add to the array
			hideindex [i] = index;
			showWord = ReplaceCharacterAtIndex (showWord, index, "_");
		}
		Array.Sort (hideindex);
		Debug.Log ("Modified:" + showWord);

		//show word in the input field characters
		if (inputFieldsList.Count > 0) {
			for (int i = 0; i < inputFieldsList.Count; i++) {
				inputFieldsList [i].transform.FindChild ("Placeholder").GetComponent<Text> ().text = showWord [i].ToString ();
				if (!showWord [i].ToString ().Equals ("_")) {
					inputFieldsList [i].enabled = false;
				}
			}
		}
	}

	void EvaluateUserInput (char userinput)
	{
		if (mistakecounter < 6 && currentindex < hideindex.Length) {
			
			int orgIndexNumber = hideindex [currentindex];
			Debug.Log (" UserInput: " + userinput + "  Correct_Letter:" + originalWord [orgIndexNumber].ToString ());

			if (originalWord [orgIndexNumber].Equals (userinput)) {
				showWord = ReplaceCharacterAtIndex (showWord, orgIndexNumber, userinput.ToString ());

				if (prevInputField != currentField) {
					currentindex++;
				}
				currentField.enabled = false;
				Debug.Log ("CurrentIndex: " + currentindex + "  Word: " + showWord);

				if (originalWord.Equals (showWord)) {
					ShowPromptMessage ("Thanks for saving my life...!");
					MummyController.OpenFences ();
					//mummy run animation
					StartCoroutine (MummyController.playMummyAnimation ("Run", 1.0f));
					StartCoroutine (HideThisGame ());
					return;
				}
			} else {
				hangManBody [mistakecounter].GetComponent<Image> ().enabled = true;
				mistakecounter++;
				prevInputField = currentField;
				StartCoroutine (MummyController.playMummyAnimation ("Damage", 0.01f));
			}
		} else {
			ShowPromptMessage ("You couldn't save me...!");
			//mummy death animation 
			StartCoroutine (MummyController.playMummyAnimation ("Death", 1.0f));
			StartCoroutine (HideThisGame ());
		}
	}

	IEnumerator HideThisGame ()
	{
		yield return new WaitForSeconds (1.0f);
		//Hide this freeman game
		freemanRef.SetActive (false);
	}

}