﻿using UnityEngine;
using System.Collections;

public class OnQuitClick : MonoBehaviour {

	void OnPress()
	{
		Application.Quit();
	}
	public void onHover()
	{
		if (!gameObject.audio.isPlaying)
		{
			gameObject.audio.Play();
		}
	}
}
