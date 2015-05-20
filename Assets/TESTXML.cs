using UnityEngine;
using System.Collections;

public class TESTXML : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameDataManager GDM=(GameDataManager)GameObject.Find("camera").GetComponent("GameDataManager");
		Debug.Log(GDM.gameData.MusicVolume);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
