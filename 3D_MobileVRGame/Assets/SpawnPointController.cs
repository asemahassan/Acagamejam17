using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnPointController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpawnItemsForGamePhase (GamePhase.Forest);
	}
	
	internal void SpawnItemsForGamePhase(GamePhase phase) {

		int amountSpwnPnts = 0;
		GameObject spwnParent = new GameObject ();

		switch (phase) {
		case GamePhase.Forest:
			{
				amountSpwnPnts = UnityEngine.Random.Range (3, 4);
				spwnParent = GameObject.Find ("Phase1");
				break;
			}

		case GamePhase.City:
			{
				
				amountSpwnPnts = UnityEngine.Random.Range (3, 6);
				spwnParent = GameObject.Find ("Phase1");
				break;
			}
		
		case GamePhase.Island:
			{
				amountSpwnPnts = UnityEngine.Random.Range (2, 5);
				spwnParent = GameObject.Find ("Phase1");
				break;
				
			}
		default:
			break;
		}
			SpawnItems (amountSpwnPnts, spwnParent);

	}

	void SpawnItems(int amountOfPoints, GameObject parent) {

		List<Transform> transforms = parent.GetComponentsInChildren<Transform> ().ToList ();
		//remove the parent transform, which is on index 0
		transforms.RemoveAt(0);

		for (int i = amountOfPoints; i >= 0; i--) {
			GameObject Quizmarker = Instantiate (Resources.Load("Prefabs/quizmarker")) as GameObject;
			int which = UnityEngine.Random.Range (0, transforms.Count);
			Quizmarker.transform.position = transforms[which].position;
			transforms.RemoveAt (which); //so it can't spawn two there

			}

	}

}
