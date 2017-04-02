using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
	[SerializeField]
	GameObject freemanGameRef = null;

	[SerializeField]
	GameObject mummyFences = null;

	private static GameObject mummyObject = null;

	private static Vector3 targetPosition = new Vector3 (625, 0, 465);

	private static control_script mummyCtrl = null;
	private static Animation animFence = null;

	public static bool isFreeman = false;

	private static BoxCollider myCollider = null;
	// Use this for initialization
	void Start ()
	{
		if (mummyFences != null)
			animFence = mummyFences.GetComponent<Animation> ();

		if (mummyObject == null) {
			mummyObject = GameObject.FindGameObjectWithTag ("Mummy");
		}

		if (mummyCtrl == null) {
			mummyCtrl = mummyObject.GetComponent<control_script> ();
		}
		if (myCollider == null) {
			myCollider = this.GetComponent<BoxCollider> ();
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

			if (!isFreeman) {
				//If player selects Yes! to Help then open the puzzle to guess word
				ShowFreeManGame ();
			}
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
		if (animFence != null) {
			animFence.Play ();
		}
		isFreeman = true;

		GameController.UpdateQuestItemsCount (QuestType.Freeman);
		if (myCollider != null) {
			myCollider.enabled = false;
		}

	}

	public static IEnumerator playMummyAnimation (string animType, float delay)
	{

		yield return new WaitForSeconds (delay);

		switch (animType) {
		case "Idle":
			{
				mummyCtrl.OtherIdle ();
				break;
			}
		case "Strike":
			{
				mummyCtrl.Strike ();
				yield return new WaitForSeconds (1.0f);
				mummyCtrl.Run ();
				targetPosition = new Vector3 (targetPosition.x, mummyObject.transform.position.y, targetPosition.z);
				iTween.MoveTo (mummyObject, iTween.Hash ("position", targetPosition, "time", 30.0f, "easetype", iTween.EaseType.linear, 
					"oncompleted", "GoToIdle", "oncompletetarget", mummyObject));
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

	private void GoToIdle ()
	{
		StartCoroutine (MummyController.playMummyAnimation ("Idle", 0.5f));
	}
}

