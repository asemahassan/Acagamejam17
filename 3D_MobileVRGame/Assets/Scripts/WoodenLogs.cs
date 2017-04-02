using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLogs : MonoBehaviour
{
	PlayerController playerCtrl = null;

	[SerializeField]
	AudioSource _audio = null;

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

	void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Player")) {

			// play wood sfx 
			if (!_audio.isPlaying) {
				_audio.PlayOneShot (_audio.clip, 1.0f);
			}

			GameController.UpdateQuestItemsCount (QuestType.Woods);
			Destroy (this.gameObject, 1.0f);
		}

	}
}
