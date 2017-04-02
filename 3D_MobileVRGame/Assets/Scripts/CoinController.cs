using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	[SerializeField]
	AudioSource _audio = null;
	// Use this for initialization
	void Start ()
	{
		if (_audio == null) {
			_audio = this.GetComponent<AudioSource> ();
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate (0, Time.deltaTime * 100, 0);
	}

	void OnTriggerEnter (Collider col)
	{

		if (col.tag.Equals ("Player")) {
			// play ting sfx 
			if (!_audio.isPlaying) {
				_audio.PlayOneShot (_audio.clip, 1.0f);
			}
			//update score for coin
			GameController.UpdateQuestItemsCount (QuestType.Coins);
			//destroy this coin
			Destroy (this.gameObject, 1.0f);

		}
		
	}

}
