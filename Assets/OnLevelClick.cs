using UnityEngine;
using System.Collections;

public class OnLevelClick : MonoBehaviour {

	public AudioClip beep;
	public int level;

	public void OnPress()
	{
		audio.PlayOneShot (beep);
//		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel (level);
		print ("loadlevel1");
//		Screen.showCursor = false;
		//IEnumerator
	}
}
