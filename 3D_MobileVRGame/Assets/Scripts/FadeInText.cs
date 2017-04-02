using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeText());
	}
	
	IEnumerator FadeText() {
		yield return new WaitForSeconds(6);

		Text text = this.GetComponentsInChildren<Text> () [0];
		Color textColor = new Color ();
		textColor.a = 0;
		text.CrossFadeColor (textColor, 3, false, true);


		
	}
}
