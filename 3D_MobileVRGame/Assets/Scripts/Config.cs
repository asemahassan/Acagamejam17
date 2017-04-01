
﻿using UnityEngine;
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
	public static Dictionary<TKey, TValue> Shuffle<TKey, TValue> (
		this Dictionary<TKey, TValue> source)
	{
		System.Random r = new System.Random ();
		return source.OrderBy (x => r.Next ())
           .ToDictionary (item => item.Key, item => item.Value);
	}
}

public static class Extensions
{
	public static string ReplaceAt (this string value, int index, char newchar)
	{
		if (value.Length <= index)
			return value;
		else
			return string.Concat (value.Select ((c, i) => i == index ? newchar : c));
	}
}


/*
	/// <summary>
/// Replace a string char at index with another char
/// </summary>
/// <param name="text">string to be replaced</param>
/// <param name="index">position of the char to be replaced</param>
/// <param name="c">replacement char</param>
public static string ReplaceAtIndex (this string text, int index, char c)
{
	var stringBuilder = new StringBuilder (text);
	stringBuilder [index] = c;
	return stringBuilder.ToString ();
}
*/

public enum GamePhase
{
	//levels
	Forest = 0,
	City = 1,
	Island = 2
}

public enum PlayerState
{
	None = 0,
	Idle,
	Dialog,
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
