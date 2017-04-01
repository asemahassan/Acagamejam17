﻿using UnityEngine;
using System.Collections;

public class examplePlayer : MonoBehaviour
{
    //This script handles player movement and interaction with other NPC game objects

    //Reference to our diagUI script for quick access
    public exampleUI diagUI;
	bool convoHasBegun;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        //Only allow player to move and turn if there are no dialogs loaded
        if (!VIDE_Data.isLoaded)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * 5, 0);
            float move = Input.GetAxisRaw("Vertical");
            transform.position += transform.forward * 5 * move * Time.deltaTime;
        }
        //Interact with NPCs when hitting spacebar
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            TryInteract();
//        }
        //Hide/Show cursor
		if (convoHasBegun) {
			if (Input.GetKeyDown(KeyCode.Return) || (Input.GetKeyDown(KeyCode.Space)) ) {
				//If conversation already began, let's just progress through it
				diagUI.CallNext();
			}
		}
    }

    //Casts a ray to see if we hit an NPC and, if so, we interact
	void TryInteract(Collision col)
    {
//        RaycastHit rHit;
//
//        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
//        {
//            //In this example, we will try to interact with any collider the raycast finds
//
//            //Lets grab the NPC's DialogueAssign script... if there's any
            VIDE_Assign assigned;
//            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
		    assigned = col.gameObject.GetComponent<VIDE_Assign>();
//            else return;
//               
            if (!VIDE_Data.inScene)
            {
                Debug.LogError("No VIDE_Data component in scene!");
                return;
            }       

            if (!VIDE_Data.isLoaded)
            {
                //... and use it to begin the conversation
                diagUI.Begin(assigned);
			convoHasBegun = true;
            }
            else
            {
                
               
            }

       // }
    }

	void OnCollisionEnter(Collision col) {
	Debug.Log("collision");
		TryInteract (col);
	}

}
