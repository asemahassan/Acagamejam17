using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	[SerializeField]
	private GameObject playerNameObj = null;
	[SerializeField]
	private GameObject panelMsg = null;
	[SerializeField]
	private AudioSource _audio = null;

	private string playerName = "";
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlayerName ()
	{
		if (playerNameObj != null) {
			playerName = playerNameObj.transform.FindChild ("InputField").transform.FindChild ("Text").GetComponent<Text> ().text;
			PlayerPrefs.SetString ("PlayerName", name);
		}
	
		//open panel for the game message
		StartCoroutine (OpenMessagePanel ());
	}

	private void PlayMenuClick ()
	{
		if (_audio != null && !_audio.isPlaying) {
			_audio.PlayOneShot (_audio.clip, 0.5f);
		}
	}

	private IEnumerator OpenMessagePanel ()
	{
		PlayMenuClick ();

		yield return new WaitForSeconds (1.0f);
		if (panelMsg != null) {

			panelMsg.transform.FindChild ("Text").GetComponent<Text> ().text = playerName.ToUpper () + " !\n \n" +
			"WHAT DO YOU WANT FROM LIFE? \n\n" +
			" LET's EXPLORE...";
			if (playerNameObj != null) {
				playerNameObj.SetActive (false);
			}


			if (panelMsg.GetComponent<Animator> ().enabled != true)
				panelMsg.GetComponent<Animator> ().enabled = true;

			bool isHidden = panelMsg.GetComponent<Animator> ().GetBool ("isHidden");
			if (isHidden) {
				panelMsg.GetComponent<Animator> ().SetBool ("isHidden", false);
			}
		}

		yield return new WaitForSeconds (2.0f);
			
		StartGame ();
	}

	private void CloseMessagePanel ()
	{
		bool isHidden = panelMsg.GetComponent<Animator> ().GetBool ("isHidden");
		if (!isHidden)
			panelMsg.GetComponent<Animator> ().SetBool ("isHidden", true);
		
	}

	public void StartPlayerName ()
	{
		StartCoroutine (LoadSceneWithDelay (1, 2.0f));
	}

	public void StartGame ()
	{
		StartCoroutine (LoadSceneWithDelay (2, 1.0f));
	}

	IEnumerator LoadSceneWithDelay (int sceneName, float delay)
	{
		yield return new WaitForSeconds (delay);
		SceneManager.LoadScene (sceneName);
	}

	public void QuitApplication ()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit ();
	}
}
