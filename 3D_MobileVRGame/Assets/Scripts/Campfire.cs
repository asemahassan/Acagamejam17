using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

	PlayerController playerCtrl = null;
	bool isFire = false;
	[SerializeField]
	AudioSource _audio = null;

	public List<GameObject> fireChilds = new List<GameObject> ();

	// Use this for initialization
	void Start ()
	{
		if (_audio == null) {
			_audio = this.GetComponent<AudioSource> ();
		}
		if (playerCtrl == null) {
			playerCtrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}

	void Update ()
	{
		int woodCounter = GameController.GetQuestCounterValueForKey (QuestType.Woods.ToString ());
		if (woodCounter == 6 && !isFire) {
			MakeCampFire ();
			isFire = true;
		}
	}

	public void MakeCampFire ()
	{
		// play campfire in loop 
		if (!_audio.isPlaying) {
			_audio.Play ();
		}
		if (fireChilds.Count > 0) {
			for (int k = 0; k < fireChilds.Count; k++) {
				GameObject obj = fireChilds [k];
				obj.SetActive (true);
			}
		}
		GameController.UpdateQuestItemsCount (QuestType.Fire);
	}
}
