using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnPointController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	internal void SpawnItemsForGamePhase(GamePhase phase) {

		int amountSpwnPnts;
		GameObject spwnParent;

		switch (phase) {
		case GamePhase.Forest:
			{
				amountSpwnPnts = UnityEngine.Random (3, 8);
				spwnParent = GameObject.Find ("Phase1");
				break;
			}

		case GamePhase.City:
			{
				amountSpwnPnts = UnityEngine.Random (3, 6);
				spwnParent = GameObject.Find ("Phase1");
				break;
			}
		
		case GamePhase.Island:
			{
				amountSpwnPnts = UnityEngine.Random (2, 5);
				spwnParent = GameObject.Find ("Phase1");
				break;
				
			}
		}

		SpawnItems (amountSpwnPnts, spwnParent);
	}

	void SpawnItems(int amountOfPoints, GameObject parent) {

		List<Transform> transforms = parent.GetComponentsInChildren<Transform> ().ToList ();
		//remove the parent transform, which is on index 0
		transforms.RemoveAt(0);

		for (int i = 0; i < amountOfPoints; i++) {
			GameObject Quizmarker = GameObject.Instantiate (Resources.Load ("/Prefabs/quizmarker"));
			Quizmarker.transform.position = transforms [UnityEngine.Random (0, transforms.Count)];

			}

	}

}
