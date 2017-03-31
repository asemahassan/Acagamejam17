using System;
using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    #region VARIABLES
    public static PlayerState _playerState = PlayerState.None;
    public static InputDevice _inputDevice = InputDevice.None; //keyboard 
    public static HMD _hmd = HMD.None;

    private GameObject player = null;
    private GameObject mainCamera = null;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    private void Awake()
    {
        //read all the data from the PlayerPrefs necessary to set environment
        GetActiveDisplayDevice();
        GetActiveInputDevice();
    }

    private void Start()
    {
        _playerState = PlayerState.Idle;
    }
    #endregion

    private void GetActiveInputDevice(){

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            string activeDevice = names[x];
            Debug.Log(activeDevice);
            if (activeDevice.Equals("Controller(Xbox One For Windows)"))
            {
                _inputDevice = InputDevice.XboxOne;
            }
            _inputDevice = InputDevice.None;

        }
    }
    private void GetActiveDisplayDevice()
    {
        //1. Move player according to device loaded
        string loadedVRDevice = VRSettings.loadedDeviceName;
        Debug.Log("LoadedDeviceName: " + loadedVRDevice);
        //2- This works for both HTC or Oculus
        if (loadedVRDevice == HMD.OpenVR.ToString())
        {
            _hmd = HMD.OpenVR;
        }
        //3. This works for Oculus Rift, GearVR...
        else if (loadedVRDevice == HMD.Oculus.ToString())
        {
            _hmd = HMD.Oculus;
        }
        //4. No VR device, use simple player with Desktop Display
        else
        {
            _hmd = HMD.None;
        }
    }

}
