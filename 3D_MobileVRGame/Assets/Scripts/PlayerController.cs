﻿/*PlayerController.cs
* Controls the movement of player according to the device active
* IF none is selected from Menu 
* Then EditorPlayer: Player moves forward and can rotate
* For any HMD: Rotation is controller from it and Player can only move forward in direction of camera using Xbox controller.
 */

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
	#region VARIABLES

	//speed in VR mode should be less to avoid nausea
	[SerializeField]
	private float speed = 10.0F;
	[SerializeField]
	private float rotateSpeed = 5.0F;
	private CharacterController controller = null;
	private Transform mainCamera = null;
	public int huntDown = 0;

	public int moneyGiven = 0;

	[SerializeField]
	private GameObject swordObject = null;
	private Animation swordCtrl = null;

	public static Vector3 _huntingAnimalPos = Vector3.zero;

	#endregion

	#region UNITY_METHODS

	[SerializeField]
	AudioSource _audio = null;

	//Use this for initialisation
	private void Start ()
	{
		if (mainCamera == null) {
			mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		}

		if (controller == null) {
			controller = GetComponent<CharacterController> ();
		}
		if (_audio == null) {
			_audio = this.GetComponent<AudioSource> ();
		}
	}

	void Update ()
	{
		if (GameController._hmd == HMD.None) {
			if (GameController._playerState == PlayerState.Idle) {
				float horizontal = 0;
				float vertical = 0;

				if (GameController._inputDevice == InputDevice.None) {
					horizontal = Input.GetAxis ("Horizontal");
					vertical = Input.GetAxis ("Vertical");
					//Can rotate in any direction
					mainCamera.transform.Rotate (0, horizontal * rotateSpeed, 0);
					Vector3 forward = transform.TransformDirection (mainCamera.transform.forward);
					float curSpeed = speed * vertical;
					controller.SimpleMove (forward * curSpeed);
				}

			} else if (GameController._playerState == PlayerState.Hunt) {
				ShowSword ();
				if (huntDown <= 4) {
					//do sword action, using X key
					if (Input.GetKeyUp (KeyCode.X)) {
						if (!swordCtrl.isPlaying) {
							swordCtrl.Play ();

							// play cutting sfx 
							if (!_audio.isPlaying) {
								_audio.PlayOneShot (_audio.clip, 1.0f);
							}
							GameController.CreateMeatObject (_huntingAnimalPos);
							huntDown++;
							Debug.Log ("Hunt down: " + huntDown);
						}
					}
				}
			}

		} else if (GameController._hmd == HMD.Oculus || GameController._hmd == HMD.OpenVR) {
			//rotation is directly from HMD, can only move forward when in IDLE state
			if (GameController._playerState == PlayerState.Idle) {
				float vertical = 0;
				if (GameController._inputDevice == InputDevice.None) {
					vertical = Input.GetAxis ("Vertical");
					Vector3 forward = transform.TransformDirection (mainCamera.transform.forward);

					float curSpeed = speed * vertical;
					controller.SimpleMove (forward * curSpeed);
				} else if (GameController._inputDevice == InputDevice.XboxOne) {
					vertical = Input.GetAxis ("Oculus_GearVR_LThumbstickY");
					Vector3 forward = transform.TransformDirection (mainCamera.transform.forward);
					float curSpeed = speed * vertical;
					controller.SimpleMove (forward * curSpeed);
				}
			}
		}
	}

	public void ShowSword ()
	{
		//activate sword
		if (swordObject != null) {
			if (!swordObject.activeSelf) {
				swordObject.SetActive (true);
				if (swordCtrl == null) {
					swordCtrl = swordObject.GetComponent<Animation> ();
				}						

			}
		}
	}

	public void HideSword ()
	{
		//activate sword
		if (swordObject != null) {
			if (swordObject.activeSelf) {
				swordObject.SetActive (false);
			}
		}
	}

	#endregion

}