using UnityEngine;
using System.Collections;
//using  System.Threading;

public class LevelController : MonoBehaviour {
	public AudioClip beep;
	void Start () {
		Screen.showCursor = true;
	}
	public void OnLevel1Click()
	{
		audio.PlayOneShot (beep);
//		Thread.Sleep(1*1000);
//		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel (1);
//		Screen.showCursor = false;
	}
	
	public void OnLevel2Click()
	{		
		audio.PlayOneShot (beep);
//		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel (1);
	}
	
	public void OnLevel3Click()
	{
		audio.PlayOneShot (beep);
//		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel (1);
	}
}