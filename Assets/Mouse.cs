using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MouseFalse()
	{
		Screen.showCursor = false;
	}

	public void MouseTrue()
	{
		Screen.showCursor = true;
	}
}
