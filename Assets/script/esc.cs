using UnityEngine;
using System.Collections;

public class esc : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt("fuck",1);
		//Debug.Log(PlayerPrefs.GetInt("fuck"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape))
		{
			Application.Quit();
		}		

	}
}
