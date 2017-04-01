using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Animation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, Time.deltaTime*100, 0);
	}
}
