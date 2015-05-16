using UnityEngine;
using System.Collections;

public class boommm : MonoBehaviour {
	public GameObject boomfabs;
	public GameObject boomaudiofabs;
	public GameObject boomfirefabs;

	private GameObject master;
	private float power;
	private float lefttime;

	void Start () {	
	}	
	void Update () {
		lefttime=lefttime-Time.deltaTime;
		if(lefttime<0)
		{
			Destroy(this.gameObject);
		}
	}
	void OnDestroy()
	{
		if (master!=null)
		{
			PutBomb pp=(PutBomb)master.GetComponent("PutBomb");
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
			boom.tag="boom";
			boom.rigidbody.AddForce(1000,0,0);
			Destroy(boom,power/20.0f);
		}
		
		{
			Vector3 position=new Vector3(transform.position.x-0.3f,transform.position.y+0.5f,transform.position.z);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom";
			boom.rigidbody.AddForce(-1000,0,0);
			Destroy(boom,power/20.0f);

		}
		
		{
			Vector3 position=new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z+0.3f);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom";
			boom.rigidbody.AddForce(0,0,1000);

			Destroy(boom,power/20.0f);
		}
		
		{
			Vector3 position=new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z-0.3f);
			GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
			boom.tag="boom";
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
	public void setmaster(GameObject temp)
	{
		master=temp;
	}
}
