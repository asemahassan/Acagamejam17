using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartDemo()
    {
        //To update list of sdk's for VR, this works only in editor
      //  string[] priorityList = {HMD.OpenVR.ToString(), HMD.None.ToString()};
      //  UnityEditorInternal.VR.VREditor.SetVREnabledDevices(UnityEditor.BuildTargetGroup.Standalone, priorityList);
        StartCoroutine(LoadSceneWithDelay());
    }
    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
