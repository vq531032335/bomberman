using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject win;
	public GameObject failWindow1;
	public GameObject failWindow2;
	private bool tag=false;
	// Use this for initialization
	void Start () {	
		win.SetActive (false);
		failWindow1.SetActive (false);
		failWindow2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{		
			tag=!tag;
		}
		if(tag==true)
		{
			Time.timeScale=0;
			GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=false;
			Screen.showCursor = true;
		}
		else
		{
			Time.timeScale=1;
			GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=true;
			Screen.showCursor = false;
		}
		win.SetActive (tag);

		int life = GameObject.FindGameObjectWithTag ("Player").GetComponent<PutBomb> ().life;
		float time = GameObject.Find("TopCamera").GetComponent<CountDown> ().Time1;
		if(life==0)
		{
			failWindow1.SetActive (true);
			Time.timeScale=0;
			GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=false;
			Screen.showCursor = true;
		}

		if(time<=0)
		{
			failWindow2.SetActive (true);
			Time.timeScale=0;
			GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=false;
			Screen.showCursor = true;
		}
	}

	public void OnMenuClick()
	{
		tag=!tag;
		win.SetActive (tag);
		if(tag==true)
		{
			Time.timeScale=0;
			Screen.showCursor = true;
		}
		else
		{
			Time.timeScale=1;
			Screen.showCursor = false;
		}
	}

	public void OnReturnClick()
	{
		Application.LoadLevel(0);
		print ("loadlevel0");
	}

	public void OnQuitClick()
	{
		Application.Quit();
	}
}
