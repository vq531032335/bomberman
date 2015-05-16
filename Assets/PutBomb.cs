using UnityEngine;
using System.Collections;

public class PutBomb : MonoBehaviour {
	public GameObject bombfabs;
	public int Maxbomb;
	private int nowbomb;
	public float power;
	public int life;
	private bool death;
	public float lefttime;
	// Use this for initialization
	void Start () {
		nowbomb=0;
		death=false;
	}
	
	// Update is called once per frame
	void Update () {
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
		life--;
		if (tag=="Player")
		{
			particleSystem.Play();
		}
		if(life<=0)
		{
			death=true;
		}
		Debug.Log(gameObject.name);
	}
	public void addlife()
	{
		life++;
	}
	public bool bombenable()
	{
		return Maxbomb>nowbomb;
	}
}
