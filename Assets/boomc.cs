using UnityEngine;
using System.Collections;

public class boomc : MonoBehaviour {
	public GameObject item_volumefabs;
	public GameObject item_powerfabs;
	public GameObject item_speedfabs;
	public GameObject item_lifefabs;
	public int probability;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag=="stone"&&collision.gameObject.renderer.enabled==true)
		{
			Vector3 position=new Vector3(collision.gameObject.transform.position.x,0.5f,collision.gameObject.transform.position.z);
			switch(Random.Range(1,probability))
			{
			case 1:
				GameObject item1=(GameObject)Instantiate(item_volumefabs,position,transform.rotation);
				break;
			case 2:
				GameObject item2=(GameObject)Instantiate(item_powerfabs,position,transform.rotation);
				break;
			case 3:
				GameObject item3=(GameObject)Instantiate(item_speedfabs,position,transform.rotation);
				break;
			case 4:
				GameObject item4=(GameObject)Instantiate(item_lifefabs,position,transform.rotation);
				break;
			}
			Destroy(collision.gameObject);
		}
		if(collision.gameObject.tag=="bomb1")
		{
			Destroy(collision.gameObject);
		}
		if(collision.gameObject.tag=="bomb2")
		{
			Destroy(collision.gameObject);
		}
		if(collision.gameObject.tag=="Player"||collision.gameObject.tag=="enemy")
		{
			PutBomb pp=(PutBomb)collision.gameObject.GetComponent("PutBomb");
			pp.minuslife();
			Debug.Log(collision.gameObject.name+"--");
		}
		if(collision.gameObject.name!="Space")
		{
			Destroy(this.gameObject);
		}
	}
}
