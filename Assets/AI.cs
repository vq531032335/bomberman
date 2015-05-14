using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	private CharacterController controller;
	private PutBomb putbomb;

	private GameObject player;

	private int State;
	public const int STATE_MOVE=0;
	public const int STATE_WANDER=1;

	public float reacttime;//interval of reacting
	private float duringtime;

	private float puttime;//interval of putting bombs
	private float duringtime2;
	private float neardistance;

	public float speed;
	private Vector3 destination;
	private Vector3 direction;
	private Vector3 lastposition;
	private GameObject targetitem;

	public float wandertime;
	private float nowwandertime;

	public float escapetime;
	private float nowescapetime;

	public float ignoretime;
	private float nowignoretime;

	public float sensitivity;
	public float intell;
	public float aggressive;

	void Start () {
		player=GameObject.Find("aircraft");
		State=STATE_WANDER;

		puttime=1.0f;
		duringtime=0;
		duringtime2=0;
		neardistance=5.0f;

		destination=transform.position;
		direction=transform.forward;
		lastposition=transform.position;
		targetitem=null;
		nowignoretime=0.0f;
		nowwandertime=0.0f;
		nowescapetime=0.0f;

		controller=(CharacterController)gameObject.GetComponent("CharacterController");
		putbomb=(PutBomb)gameObject.GetComponent("PutBomb");
	
	}

	void Update () {
		if(nowignoretime>0)
		{
			nowignoretime=nowignoretime-Time.deltaTime;
		}
		if(nowwandertime>0)
		{
			nowwandertime=nowwandertime-Time.deltaTime;
		}
		//decision
		duringtime+=Time.deltaTime;
		if(duringtime>reacttime)
		{
			puttime=(2.0f/(speed+1.0f));
			neardistance=2.0f*(speed);
			switch(State)
			{
			case STATE_MOVE:
				if(targetitem==null)
				{
					State=STATE_WANDER;
					return;
				}
				else if(targetitem.renderer.enabled==false)
				{
					State=STATE_WANDER;
					return;
				}
				if(Vector3.Distance(transform.position,lastposition)<sensitivity*speed*Time.deltaTime||indanger())
				{
					nowignoretime=ignoretime;
					State=STATE_WANDER;
					return;
				}

				break;
			case STATE_WANDER:

				targetitem=finditem();
				if(targetitem!=null&&nowignoretime<=0)
				{
					State=STATE_MOVE;
					destination=targetitem.transform.position;
					return;
				}
				if (Random.Range(0.0f,1.0f)<aggressive/5&&nowescapetime<=0)
				{
					transform.LookAt(player.transform.position);
					break;
				}
				if(Vector3.Distance(transform.position,lastposition)<sensitivity*speed*Time.deltaTime||nowwandertime<=0||indanger()&&nowescapetime<=0)
				{
					nowwandertime=wandertime;
					nowescapetime=escapetime;
					if (Random.Range(0.0f,1.0f)<intell)
					{
						transform.Rotate(0.0f,180.0f,0.0f);
					}
					else{
						transform.Rotate(0.0f,Random.Range(0.0f,360.0f),0.0f);
					}
					wantputbomb();
				}

				break;
			}
			duringtime=0.0f;
		}
		//bomb
		duringtime2+=Time.deltaTime;
		if(nearplayer())
		{
			wantputbomb();
		}
		nowescapetime-=Time.deltaTime;

		//move
		Vector3 position=new Vector3(transform.position.x,0.8f,transform.position.z);
		transform.position=position;
		switch(State)
		{
		case STATE_MOVE:
			Vector3 temp=new Vector3(destination.x,transform.position.y,destination.z);
			transform.LookAt(temp);
			lastposition=transform.position;
			controller.Move(transform.forward*Time.deltaTime*speed);
			break;
		case STATE_WANDER:
			direction=transform.forward;
			lastposition=transform.position;
			controller.Move(direction*Time.deltaTime*speed);
			break;
		}

	}
	public void addspeed()
	{
		speed+=0.5f;
	}
	private bool nearplayer()
	{
		if(Vector3.Distance(transform.position,player.transform.position)<neardistance)
		{
			return true;
		}
		return false;
	}
	private void wantputbomb()
	{
		if(duringtime2>puttime)
		{
			putbomb.Put(2);
			duringtime2=0.0f;
		}
	}
	private bool indanger()
	{
		if(indanger2("bomb1"))
		{
			return true;
		}
		if(indanger2("bomb2"))
		{
			return true;
		}
		return false;
		//1power 1.25
	}
	private bool indanger2(string name)
	{
		GameObject[] bombs=GameObject.FindGameObjectsWithTag(name);
		foreach(GameObject bomb in bombs)
		{
			if(transform.position.x<bomb.transform.position.x+0.3f&&transform.position.x>bomb.transform.position.x-0.3f)
			{
				boommm bbb=(boommm)bomb.GetComponent("boommm");
				float power=1.3f*bbb.getpower();
				if(transform.position.y<bomb.transform.position.y+power &&transform.position.y>bomb.transform.position.y-power)
				{
					return true;
				}
			}			
		}
		return false;
	}

	private GameObject finditem()
	{
		float mindistance=100.0f;
		GameObject minitem=null;
		finditem2("item_volume",ref mindistance,ref minitem);
		finditem2("item_power",ref mindistance,ref minitem);
		finditem2("item_speed",ref mindistance,ref minitem);
		finditem2("item_life",ref mindistance,ref minitem);
		if (mindistance<10.0f)
		{
			if(Vector3.Distance(minitem.transform.position,player.transform.position)>mindistance)
			{
				return minitem;
			}
			return null;
		}
		else
		{
			return null;
		}
	}
	private void finditem2(string name,ref float mindistance,ref GameObject minitem)
	{
		GameObject[] items=GameObject.FindGameObjectsWithTag(name);
		foreach(GameObject item in items)
		{
			if(item.renderer.enabled==true)
			{
				float distance=Vector3.Distance(item.transform.position,transform.position);
				if(distance<mindistance)
				{
					mindistance=distance;
					minitem=item;
				}
			}
		}
	}


}
