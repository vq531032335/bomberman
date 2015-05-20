using UnityEngine;
using System.Collections;
//standard

public class boomc : MonoBehaviour {
	public GameObject item_volumefabs;
	public GameObject item_powerfabs;
	public GameObject item_speedfabs;
	public GameObject item_lifefabs;
	public int probability;

	void Start () {
	}

	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		//make item
		if(collision.gameObject.tag=="stone"&&collision.gameObject.renderer.enabled==true)
		{
			Vector3 position=new Vector3(collision.gameObject.transform.position.x,0.5f,collision.gameObject.transform.position.z);
			switch(Random.Range(1,probability))
			{
			case 1:
				Instantiate(item_volumefabs,position,transform.rotation);
				break;
			case 2:
				Instantiate(item_powerfabs,position,transform.rotation);
				break;
			case 3:
				Instantiate(item_speedfabs,position,transform.rotation);
				break;
			case 4:
				Instantiate(item_lifefabs,position,transform.rotation);
				break;
			}
			collision.gameObject.renderer.enabled=false;
			Destroy(collision.gameObject);
		}

		//boom bomb
		if(collision.gameObject.tag=="bomb")
		{
			boommm bbb=(boommm)collision.gameObject.GetComponent("boommm");
			bbb.go();
		}

		//minus life
		if(collision.gameObject.tag=="Player"||collision.gameObject.tag=="cruiser")
		{
			PutBomb pp=(PutBomb)collision.gameObject.GetComponent("PutBomb");
			pp.minuslife();
		}
		if(collision.gameObject.tag=="crasher")
		{
			crasherAI ai=(crasherAI)collision.gameObject.GetComponent("crasherAI");
			ai.die();
		}
		if(collision.gameObject.tag=="smoker")
		{
			smokerAI ai=(smokerAI)collision.gameObject.GetComponent("smokerAI");
			ai.die();
		}

		//dis
		if(collision.gameObject.name!="Space")
		{
			Destroy(this.gameObject);
		}
	}
}
