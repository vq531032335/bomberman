using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject pauseWindow;
	public GameObject successWindow;
	public GameObject failWindow1;
	public GameObject failWindow2;
	public GameObject camera;
	public UISprite speedroll;
	private bool status;
	private bool end;

	void Start () {	
		pauseWindow.SetActive (false);
		successWindow.SetActive (false);
		failWindow1.SetActive (false);
		failWindow2.SetActive (false);
		status=false;
		end=false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)&& end==false)
		{		
			status=!status;			
			pauseWindow.SetActive (status);
		}
		if(status)
		{
			Time.timeScale=0;
		}
		else
		{
			Time.timeScale=1;
		}
		GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=!status;
		Screen.showCursor = status;

		if (!end)
		{
			int life = GameObject.FindGameObjectWithTag ("Player").GetComponent<PutBomb> ().life;
			float time = GameObject.Find("TopCamera").GetComponent<CountDown> ().levelTime;
			ThirdPersonController TPC=(ThirdPersonController)GameObject.FindGameObjectWithTag ("Player").GetComponent("ThirdPersonController");
			if(life==0)
			{
				camera.transform.FindChild("failaudio").audio.Play();
				End(failWindow1);
			}
			if(time<=0)
			{
				camera.transform.FindChild("failaudio").audio.Play();
				End(failWindow2);
			}
			if(enemyNumber()<=0)
			{
				camera.transform.FindChild("successaudio").audio.Play();
				End(successWindow);
			}
			speedroll.width=(int)(TPC.getnowspeedup()*50);
			speedroll.transform.localPosition=new Vector3(100-25*TPC.getnowspeedup(),0,0);
		}
		else
		{
			Time.timeScale=0;
		}

	}
	private int enemyNumber()
	{
		int enemynumber=0;
		GameObject[] enemies=GameObject.FindGameObjectsWithTag("cruiser");
		foreach(GameObject enemy in enemies)
		{
			enemynumber++;
		}
		enemies=GameObject.FindGameObjectsWithTag("crasher");
		foreach(GameObject enemy in enemies)
		{
			enemynumber++;
		}
		enemies=GameObject.FindGameObjectsWithTag("smoker");
		foreach(GameObject enemy in enemies)
		{
			enemynumber++;
		}
		return enemynumber;
	}
	private void End(GameObject window)
	{
		window.SetActive (true);
		status=true;
		GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled=false;
		Screen.showCursor = true;
		end=true;
	}

	public void OnMenuClick()
	{
		status=!status;
	}

	public void OnReturnClick()
	{
		Application.LoadLevel(0);
	}

	public void OnNextClick()
	{
		if(GameDataManager.level<4)
		{			
			GameDataManager.level++;
		}
		GameDataManager.dataFileName="level"+GameDataManager.level+".dat";
		Application.LoadLevel(1);
	}

	public void OnResumeClick()
	{
		status=!status;
		pauseWindow.SetActive (status);
	}

	public void OnQuitClick()
	{
		Application.Quit();
	}
}
