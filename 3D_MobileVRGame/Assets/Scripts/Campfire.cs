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
		if (GameController._gamePhase == GamePhase.Forest && this.gameObject.name.Equals ("Campfire_phase1")) {
			int woodCounter = GameController.GetQuestCounterValueForKey (QuestType.Woods.ToString ());
			if (woodCounter == 6 && !isFire) {

				GameController.UpdatePromptMessages ("You just made a campfire in Forest, look around for it");

				MakeCampFire ();
				isFire = true;
			}
		} else if (GameController._gamePhase == GamePhase.Island && this.gameObject.name.Equals ("Campfire_phase2")) {
			int woodCounter = GameController.GetQuestCounterValueForKey (QuestType.Woods.ToString ());
			if (woodCounter == 6 && !isFire) {

				GameController.UpdatePromptMessages ("You just made a campfire in Forest, look around for it");

				MakeCampFire ();
				isFire = true;
			}
		}
	}

	private void MakeCampFire ()
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
		Invoke ("ResetWoodsCounter", 1.0f);
	}

	private void ResetWoodsCounter ()
	{
		GameController.ResetQuestCounterValueForKey (QuestType.Woods, 0);
	}
}
