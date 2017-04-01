using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_Animation : MonoBehaviour {

	public bool rise;

	// Use this for initialization
	void Start () {
		rise = true;
	}

	// Update is called once per frame
	void Update () {
		if (rise) {
			this.transform.Translate (0, Time.deltaTime * (1.0f-9.8f-this.transform.position.y), 0);
			if (this.transform.position.y >= -9.4)
				rise = false;
		} 
		else {
			this.transform.Translate (0, -Time.deltaTime / (1.0f-9.8f-this.transform.position.y)/2, 0);
			if (this.transform.position.y <= -9.8)
				rise = true;
		}

	}
}

