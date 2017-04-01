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
	//keyboard
	public static HMD _hmd = HMD.None;

	private GameObject player = null;
	private GameObject mainCamera = null;

	private static int coinCount = 0;

	#endregion

	#region UNITY_METHODS

	// Use this for initialization
	private void Awake ()
	{
		//read all the data from the PlayerPrefs necessary to set environment
//		GetActiveDisplayDevice ();
//		GetActiveInputDevice ();
	}

	private void Start ()
	{
		_gamePhase = GamePhase.Forest;
		_playerState = PlayerState.Idle;
//		SpawnCoinsInSpecifiedAreas ();
	}

	#endregion

	private void GetActiveInputDevice ()
	{

		string[] names = Input.GetJoystickNames ();
		for (int x = 0; x < names.Length; x++) {
			string activeDevice = names [x];
			Debug.Log (activeDevice);
			if (activeDevice.Equals ("Controller(Xbox One For Windows)")) {
				_inputDevice = InputDevice.XboxOne;
			}
			_inputDevice = InputDevice.None;

		}
	}

	private void GetActiveDisplayDevice ()
	{
		//1. Move player according to device loaded
		string loadedVRDevice = VRSettings.loadedDeviceName;
		Debug.Log ("LoadedDeviceName: " + loadedVRDevice);
		//2- This works for both HTC or Oculus
		if (loadedVRDevice == HMD.OpenVR.ToString ()) {
			_hmd = HMD.OpenVR;
		}
        //3. This works for Oculus Rift, GearVR...
        else if (loadedVRDevice == HMD.Oculus.ToString ()) {
			_hmd = HMD.Oculus;
		}
        //4. No VR device, use simple player with Desktop Display
        else {
			_hmd = HMD.None;
		}
	}

	public static void UpdateCoinCount ()
	{
		coinCount++;

	}

	private void SpawnCoinsInSpecifiedAreas ()
	{
		int totalPosIndexes = 0;
		Transform spwnParent = null;

		switch (_gamePhase) {
		case GamePhase.Forest:
			{
				totalPosIndexes = UnityEngine.Random.Range (3, 4);
				spwnParent = GameObject.Find ("Phase1").transform;
				break;
			}

		case GamePhase.City:
			{

				totalPosIndexes = UnityEngine.Random.Range (3, 6);
				spwnParent = GameObject.Find ("Phase2").transform;
				break;
			}

		case GamePhase.Island:
			{
				totalPosIndexes = UnityEngine.Random.Range (2, 5);
				spwnParent = GameObject.Find ("Phase3").transform;
				break;

			}
		}

		List<Transform> transforms = spwnParent.GetComponentsInChildren<Transform> ().ToList ();
		//remove the parent transform, which is on index 0
		transforms.RemoveAt (0);

		for (int i = transforms.Count; i >= 0; i--) {
			GameObject coinObj = Instantiate (Resources.Load ("Prefabs/Coin")) as GameObject;
			int which = UnityEngine.Random.Range (0, transforms.Count);
			coinObj.transform.position = new Vector3 (transforms [which].position.x + 3, transforms [which].position.y + 5, transforms [which].position.z + 5);
			transforms.RemoveAt (which); //so it can't spawn two there

		}
	}

}
