using UnityEngine;
using System.Collections;

public class OnLevelClick : MonoBehaviour {
	public GameObject press;
	public int level;
	public void OnClick()
	{
		GameDataManager.level=level;
		GameDataManager.dataFileName="level"+GameDataManager.level+".dat";
		Application.LoadLevel (1);
	}
	public void OnHover()
	{
		if (!gameObject.audio.isPlaying)
		{
			gameObject.audio.Play();
		}
	}
	public void OnPress()
	{
		if (!press.audio.isPlaying)
		{
			press.audio.Play();
		}
	}
}
