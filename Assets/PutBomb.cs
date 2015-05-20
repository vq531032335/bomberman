using UnityEngine;
using System.Collections;
//standard

public class PutBomb : MonoBehaviour {
	public GameObject bombfabs;
	public int Maxbomb;
	private int nowbomb;
	public float power;
	public int life;
	public float lefttime;

	public float invincibletime;
	private float nowinvincibletime;

	void Start () {
		nowbomb=0;
		nowinvincibletime=0;
	}
	
	void Update () {
		nowinvincibletime-=Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.Space)&& tag=="Player")
		{
			Put(1);
		}
	}
	public void Put(int type)
	{
		if(bombenable())
		{
			Vector3 position=new Vector3(transform.position.x,0.3f,transform.position.z);
			GameObject bomb=(GameObject)Instantiate(bombfabs,position,transform.rotation);
			bomb.tag="bomb";

			boommm bbb=(boommm)bomb.GetComponent("boommm");
			bbb.setmaster(this.gameObject);
			bbb.setpower(power);
			bbb.setlefttime(lefttime);
			addBomb();
			audio.Play();
		}
	}
	public void minusBomb()
	{
		nowbomb--;
	}
	public void addBomb()
	{
		nowbomb++;
	}
	public void addMaxBomb()
	{
		Maxbomb++;
	}
	public int getMaxBomb()
	{
		return Maxbomb;
	}
	public float getpower()
	{
		return power;
	}
	public void addpower()
	{
		power++;
	}
	public void minuslife()
	{
		if(nowinvincibletime<=0)
		{
			life--;
			if (tag=="Player")
			{
				particleSystem.Play();
				gameObject.transform.FindChild("cuocuo").audio.Play();
				Debug.Log(life);
			}
			if(life<=0)
			{
				if(tag=="cruiser")
				{
					die();
				}
			}
			nowinvincibletime=invincibletime;
		}
	}
	public void addlife()
	{
		life++;
	}
	public bool bombenable()
	{
		return Maxbomb>nowbomb;
	}
	private void die()//for cruiser
	{
		collider.enabled=false;
		gameObject.transform.FindChild("audio").audio.Play();		
		particleSystem.Play();
		gameObject.transform.FindChild("particle1").particleSystem.Stop();
		gameObject.transform.FindChild("particle2").particleSystem.Stop();
		this.renderer.enabled=false;
		Destroy(this.gameObject,4);
	}
}
