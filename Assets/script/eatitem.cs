﻿using UnityEngine;
using System.Collections;
//standard

public class eatitem : MonoBehaviour {
	private GameObject player;
	private GameObject[] cruisers;
	
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		caneat(player);
		cruisers=GameObject.FindGameObjectsWithTag("cruiser");
		foreach(GameObject enemy in cruisers)
		{
			caneat(enemy);
		}
		transform.Rotate(0.0f,Time.deltaTime*100,0.0f);
	}

	void caneat(GameObject temp)
	{
		if (renderer.enabled==true)
		{
			if (Vector3.Distance(transform.position,temp.transform.position)<0.7)
			{
				PutBomb pp=(PutBomb)temp.GetComponent("PutBomb");
				if(temp.tag=="Player")
				{					
					audio.Play();
				}
				this.renderer.enabled=false;
				switch(tag)
				{
				case "item_volume":
					pp.addMaxBomb();
					break;
				case "item_power":
					pp.addpower();
					break;
				case "item_speed":
					if(temp.tag=="Player")
					{
						ThirdPersonController TPC=(ThirdPersonController)temp.GetComponent("ThirdPersonController");
						TPC.plusSpeed();
						TPC.fullspeedup();
					}
					else
					{
						cruiserAI ai=(cruiserAI)temp.GetComponent("cruiserAI");
						ai.addspeed();
					}
					break;
				case "item_life":
					pp.addlife();
					break;
				}
				Destroy(this.gameObject,4);
			}
		}
	}
}
