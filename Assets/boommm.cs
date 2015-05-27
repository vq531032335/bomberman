using UnityEngine;
using System.Collections;
//standard

public class boommm : MonoBehaviour {
	public GameObject boomfabs;
	public GameObject boomaudiofabs;
	public GameObject boomfirefabs;

	private GameObject master;
	private float power;
	private float lefttime;

	private bool death;

	void Start () {	
		death=false;
	}	
	void Update () {
		lefttime=lefttime-Time.deltaTime;
		if(lefttime<0)
		{
			go();
		}
	}
	public void go()
	{
		if(death==false)
		{
			death=true;
			//return bomb
			if (master!=null)
			{
				PutBomb pp=(PutBomb)master.GetComponent("PutBomb");
				pp.minusBomb();
			}
			
			//boommmm!
			GameObject audio=(GameObject)Instantiate(boomaudiofabs,transform.position,transform.rotation);
			audio.audio.Play();
			Destroy(audio,2);
			GameObject boomfire=(GameObject)Instantiate(boomfirefabs,transform.position,transform.rotation);
			boomfire.particleSystem.Play();
			Destroy(boomfire,2);
			
			setaboom(1,0);
			setaboom(-1,0);
			setaboom(0,1);
			setaboom(0,-1);
			
			Destroy(this.gameObject);
		}

	}
	private void setaboom(int x,int y)
	{
		Vector3 position=new Vector3(transform.position.x+0.3f*x,transform.position.y+0.5f,transform.position.z+0.3f*y);
		GameObject boom=(GameObject)Instantiate(boomfabs,position,transform.rotation);			
		boom.tag="boom";
		boom.rigidbody.AddForce(0+1000*x,0,0+1000*y);
		Destroy(boom,power/20.0f);
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
