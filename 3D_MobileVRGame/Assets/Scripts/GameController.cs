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

	#endregion

	#region UNITY_METHODS

	// Use this for initialization
	private void Awake ()
	{
		if (hudObjUI == null) {
			hudObjUI = GameObject.FindGameObjectWithTag ("HUD");
		}

	}

	private void Start ()
	{
		_gamePhase = GamePhase.Forest;
		_playerState = PlayerState.Idle;
	}

	#endregion

	//For every type of quest it will update the counter in HUD when called
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
		}

	}


}
