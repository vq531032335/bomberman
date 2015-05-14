using UnityEngine;
using System.Collections;

public class boommm : MonoBehaviour {
	private GameObject player;
	private GameObject enemy;
	public GameObject boomfabs;
	public GameObject boomaudiofabs;
	public GameObject boomfirefabs;
	private float power;
	private float lefttime;
	// Use this for initialization
	void Start () {	
		player=GameObject.Find("aircraft");
		enemy=GameObject.Find("enemy");
	}	
	// Update is called once per frame
	void Update () {
		lefttime=lefttime-Time.deltaTime;
		if(lefttime<0)
		{
			Destroy(this.gameObject);
		}
	}
	void OnDestroy()
	{
		if (tag=="bomb1")
		{
			PutBomb pp=(PutBomb)player.GetComponent("PutBomb");
			pp.minusBomb();
		}
		else if(tag=="bomb2")
		{
			PutBomb pp=(PutBomb)enemy.GetComponent("PutBomb");
			pp.minusBomb();
		}
		GameObject audio=(GameObject)Instantiate(boomaudiofabs,transform.position,transform.rotation);
		audio.audio.Play();
		Destroy(audio,2);
		GameObject boomfire=(GameObject)Instantiate(boomfirefabs,transform.position,transform.rotation);
		boomfire.particleSystem.Play();
		Destroy(boomfire,2);
		
		{
			Vector3 position=new Vector3(transform.position.x+0.3f,transform.position.y+0.5f,transform.position.z);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);	
			boom.tag="boom1";
			boom.rigidbody.AddForce(1000,0,0);
			Destroy(boom,power/20.0f);
		}
		
		{
			Vector3 position=new Vector3(transform.position.x-0.3f,transform.position.y+0.5f,transform.position.z);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom1";
			boom.rigidbody.AddForce(-1000,0,0);
			Destroy(boom,power/20.0f);

		}
		
		{
			Vector3 position=new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z+0.3f);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom1";
			boom.rigidbody.AddForce(0,0,1000);

			Destroy(boom,power/20.0f);
		}
		
		{
			Vector3 position=new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z-0.3f);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom1";
			boom.rigidbody.AddForce(0,0,-1000);
			Destroy(boom,power/20.0f);
		}		
	}
	public void setpower(float p)
	{
		power=p;
	}
	public float getpower()
	{
		return power;
	}
	public void setlefttime(float t)
	{
		lefttime=t;
	}
}
