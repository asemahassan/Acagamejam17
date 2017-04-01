using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

/// <summary>
/// An extension method to shuffle the dictionary of any type
/// </summary>
public static class DictionaryExtensions
{
    public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(
       this Dictionary<TKey, TValue> source)
    {
        System.Random r = new System.Random();
        return source.OrderBy(x => r.Next())
           .ToDictionary(item => item.Key, item => item.Value);
    }
}

public enum PlayerState
{
    None = 0,
    Idle,
    Moving,
    InsideRoom,
    CenterPoint,
    AutoTranslation,
    OutsideRoom
}

public enum HMD
{
    None = -1,
    OpenVR,
    Oculus
}

public enum InputDevice
{
    None = -1,
    XboxOne,
}

public enum GamePhase { //levels
	Forest = 0,
	City = 1,
	Island = 2
}
