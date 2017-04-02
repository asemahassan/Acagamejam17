using System;
using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{

	#region VARIABLES

	public static PlayerState _playerState = PlayerState.None;
	public static GamePhase _gamePhase = GamePhase.Forest;
	public static InputDevice _inputDevice = InputDevice.None;
	public static QuestType _questType = QuestType.None;
	public static HMD _hmd = HMD.None;
	private static GameObject hudObjUI = null;

	private static Dictionary<string, int> QuestCounterData = new Dictionary<string,int> ();

	static List<Vector3> _meatSpawnPos = new List<Vector3> ();

	private static Text promptMessages = null;

	[SerializeField]
	private GameObject _phase1Exit = null;

	[SerializeField]
	private GameObject _phase2Exit = null;

	[SerializeField]
	private GameObject _phase2Entry = null;

	#endregion

	#region UNITY_METHODS

	// Use this for initialization
	private void Awake ()
	{
		if (hudObjUI == null) {
			hudObjUI = GameObject.FindGameObjectWithTag ("HUD");
		}

		if (promptMessages == null) {
			promptMessages = GameObject.FindGameObjectWithTag ("PromptMsg").GetComponent<Text> ();
		}

	}

	private void Start ()
	{
		_gamePhase = GamePhase.Forest;
		_playerState = PlayerState.Idle;
	}

	#endregion

	//Main game loop
	void Update ()
	{

		switch (_gamePhase) {

		case GamePhase.Forest:
			{
				if (QuestCounterData.Count > 0) {

					//check quest counter for 3 tasks which player has completed atleast to move further in level
					if ((QuestCounterData.ContainsKey (QuestType.Hunt.ToString ()) && QuestCounterData [QuestType.Hunt.ToString ()] >= 1)
					    && (QuestCounterData.ContainsKey (QuestType.Freeman.ToString ()) && QuestCounterData [QuestType.Freeman.ToString ()] >= 1)
					    && (QuestCounterData.ContainsKey (QuestType.Fire.ToString ()) && QuestCounterData [QuestType.Fire.ToString ()] >= 1)) {

						//Open phase 2 gate
						_phase1Exit.SetActive (false);
						GameController.UpdatePromptMessages ("Phase 2 Unlocked, explore the city");
						_gamePhase = GamePhase.City;
						_phase2Entry.SetActive (false);

					}
						
				}
				break;
			}

		case GamePhase.City:
			{
				if (QuestCounterData.Count > 0) {

					//check quest counter for 3 tasks which player has completed atleast to move further in level
					if ((QuestCounterData.ContainsKey (QuestType.Beggar.ToString ()) && QuestCounterData [QuestType.Beggar.ToString ()] >= 1)
					    && (QuestCounterData.ContainsKey (QuestType.Dog.ToString ()) && QuestCounterData [QuestType.Dog.ToString ()] >= 1)
					    && (QuestCounterData.ContainsKey (QuestType.Treasure.ToString ()) && QuestCounterData [QuestType.Treasure.ToString ()] >= 1)) {

						//Open phase 2 gate
						_phase2Exit.SetActive (false);
						GameController.UpdatePromptMessages ("Phase 3 Unlocked, explore the island");
						_gamePhase = GamePhase.Island;

					}
				}
				break;
			}

		case GamePhase.Island:
			{

				break;
			}

		}
		
	}

	public static void UpdatePromptMessages (string msg)
	{
		if (promptMessages != null) {
			promptMessages.text = msg.ToUpper ();
		}
	}

	//For every type of quest it will update the counter in HUD when called, incrementing
	public static void UpdateQuestItemsCount (QuestType type)
	{
		Text counterUI = null;
		if (hudObjUI != null) {
			if (counterUI == null) {
				counterUI = hudObjUI.transform.FindChild (type.ToString ()).transform.FindChild ("Counter").GetComponent<Text> ();
	
			}
			int count = Int32.Parse (counterUI.text);
			count++;
			counterUI.text = count.ToString ();
			if (QuestCounterData.ContainsKey (type.ToString ())) {
				QuestCounterData [type.ToString ()] = count;
				Debug.Log ("Existing:" + type.ToString () + " :" + count);
			} else {
				QuestCounterData.Add (type.ToString (), count);
				Debug.Log ("AddedNew:" + type.ToString () + " :" + count);
			}
		}
	}

	public static int GetQuestCounterValueForKey (string key)
	{
		if (QuestCounterData.Count > 0) {
			if (QuestCounterData.ContainsKey (key)) {
				return QuestCounterData [key];
			}
		}
		return 0;
	}

	public static void ResetQuestCounterValueForKey (QuestType type, int value)
	{
		Text counterUI = null;
		if (hudObjUI != null) {
			if (counterUI == null) {
				counterUI = hudObjUI.transform.FindChild (type.ToString ()).transform.FindChild ("Counter").GetComponent<Text> ();

			}
			counterUI.text = value.ToString ();
			if (QuestCounterData.ContainsKey (type.ToString ())) {
				QuestCounterData [type.ToString ()] = value;
				Debug.Log ("Reseting:" + type.ToString () + " :" + value);
			}
		}
	}


	public static void  CreateMeatObject (Vector3 pos)
	{
		pos = new Vector3 (pos.x + UnityEngine.Random.Range (-2, 2),
			pos.y, pos.z + UnityEngine.Random.Range (-2, 2));

		if (_meatSpawnPos.Count > 0) {
			while (!_meatSpawnPos.Contains (pos)) {
				pos = new Vector3 (pos.x + UnityEngine.Random.Range (-2, 2),
					pos.y, pos.z + UnityEngine.Random.Range (-2, 2));
			} 
			_meatSpawnPos.Add (pos);
		}
	
		GameObject obj = Instantiate (Resources.Load ("Prefabs/Meat")) as GameObject;
		obj.transform.position = pos;
	}
}
