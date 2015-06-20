using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	void Start () {
		Screen.showCursor = false;
	}

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
