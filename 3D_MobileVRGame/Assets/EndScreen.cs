using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Image img = gameObject.GetComponentInChildren<Image> ();
		Text txt = gameObject.GetComponentInChildren<Text> ();


		string strg = "rich";

	//	switch (GameObject.Find("Quiz").GetComponent<ResultController>().EvaluateAnswers()) {
		switch (strg) {
		case "rich": 
			img.overrideSprite = Resources.Load("ending/rich") as Sprite;
			txt.text = "You got rich in your life time, which is something lots of people want. However, do remember that money cannot buy happiness. You do not want to end up alone like Mr Gatsby here, do you?";
			break;
			
		case "adventure":
			img.sprite = Resources.Load<Sprite>("ending/adventure");
			txt.text = "Adventure is all you need. You, and nature. Who needs money, a house, other people even? I envy you, for you must have the greatest of memories and experiences. I hope you have someone to share those with.";
			break;
		case "family":
			img.sprite = Resources.Load ("ending/family") as Sprite;
			txt.text = "You belong right in the midst of your loved ones. You take care of the bruises, the hunger and the sadness. You are the rock for the people around you. Make sure tho, that even you need someone to lean on, once in a while. Dont let them use you. ";
			break;
		default:
			break;
		}


		txt.enabled = true;
		img.enabled = true;

		img.CrossFadeAlpha (1, 5, true);
		txt.CrossFadeColor (Color.white, 5, true, false);

	}
	
}
