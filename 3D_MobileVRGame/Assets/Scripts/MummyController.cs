using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
	[SerializeField]
	GameObject freemanGameRef = null;

	[SerializeField]
	GameObject mummyFences = null;

	[SerializeField]
	GameObject mummyObject = null;

	private static control_script mummyCtrl = null;
	private static Animation animFence = null;
	// Use this for initialization
	void Start ()
	{
		if (mummyFences != null)
			animFence = mummyFences.GetComponent<Animation> ();

		if (mummyCtrl == null) {
			mummyCtrl = mummyObject.GetComponent<control_script> ();
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.tag.Equals ("Player")) {
			//Start the mummy convo system

			//If player selects Yes! to Help then open the puzzle to guess word
			ShowFreeManGame ();
		}
		
	}

	void ShowFreeManGame ()
	{
		if (!freemanGameRef.activeSelf) {
			freemanGameRef.SetActive (true);
		} else {
			freemanGameRef.SetActive (false);
		}
			
			
	}

	public static void OpenFences ()
	{
		if (animFence != null)
			animFence.Play ();
	}

	public static IEnumerator playMummyAnimation (string animType, float delay)
	{

		yield return new WaitForSeconds (delay);
		switch (animType) {
		case "Run":
			{
				mummyCtrl.Run ();
				break;
			}
		case "Attack":
			{
				mummyCtrl.Attack ();
				break;
			}
		case "Death":
			{
				mummyCtrl.Death ();
				break;
			}
		case "Damage":
			{
				mummyCtrl.Damage ();
				break;
			}
			
		}

	}
}

