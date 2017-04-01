using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat_Animation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (Time.deltaTime*100, 0, 0);
	}
}
