using UnityEngine;
using System.Collections;
//standard

public class smokerAI : MonoBehaviour {
	private CharacterController controller;
	public float speed;
	private float rantime;
	private bool nowaudioon;

	private GameObject target;
	public float attacktime;//interval of damage
	private float nowattacktime;
	public float attackdistance;

	public Vector3 point1;//patrol between the 2 points
	public Vector3 point2;

	private int status;//controll patrol

	private bool death;

	void Start () {
		controller=(CharacterController)gameObject.GetComponent("CharacterController");
		rantime=Random.Range(0.0f,3.0f);
		nowaudioon=false;
		target=GameObject.FindGameObjectWithTag("Player");
		nowattacktime=0.0f;
		status=0;
		death=false;
	}

	void Update () {
		if (Time.time>rantime &&nowaudioon==false)
		{
			nowaudioon=true;
			gameObject.transform.FindChild("audio").audio.Play();
		}
		if (death==false)
		{			
			//smoke
			nowattacktime-=Time.deltaTime;
			if (nowattacktime<=0)
			{
				nowattacktime=attacktime;
				if (Vector3.Distance(transform.position,target.transform.position)<attackdistance)
				{
					PutBomb pp=(PutBomb)target.GetComponent("PutBomb");
					pp.minuslife();
				}
			}
			
			//move
			switch(status)
			{
			case 0:
				if(Vector3.Distance(transform.position,point1)<1.0f)
				{
					status=1;
				}
				else
				{
					Vector3 position1=new Vector3(point1.x,transform.position.y,point1.z); 
					transform.LookAt(position1);
				}
				break;
			case 1:
				if(Vector3.Distance(transform.position,point2)<1.0f)
				{
					status=0;
				}
				else
				{
					Vector3 position2=new Vector3(point2.x,transform.position.y,point2.z); 
					transform.LookAt(position2);
				}
				break;
			}
			Vector3 direction=transform.forward;
			controller.Move(direction*Time.deltaTime*speed);
			
			//drift
			Vector3 position=transform.position;
			transform.position=new Vector3(position.x,Mathf.Sin(Time.time*3.0f+rantime)/10.0f+2.0f,position.z);
		}
	}

	public void die()
	{
		death=true;
		collider.enabled=false;
		gameObject.transform.FindChild("particle").particleSystem.Stop();
		audio.Play();
		this.renderer.enabled=false;
		Destroy(this.gameObject,2);
	}

	public void setPoint(Vector3 position1,Vector3 position2)
	{
		point1=position1;
		point2=position2;
	}
}
