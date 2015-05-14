using UnityEngine;
using System.Collections;

public class eatitem : MonoBehaviour {
	private GameObject player;
	private GameObject enemy;

	// Use this for initialization
	void Start () {
		player=GameObject.Find("aircraft");
		enemy=GameObject.Find("enemy");
	}
	
	// Update is called once per frame
	void Update () {
		caneat(ref player);
		caneat(ref enemy);			
	}

	void caneat(ref GameObject temp)
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
					}
					else
					{
						AI ai=(AI)temp.GetComponent("AI");
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
