using UnityEngine;
using System.Collections;

public class cruiserAI : MonoBehaviour {	
	private CharacterController controller;
	private PutBomb putbomb;
	
	private GameObject player;
	
	private int State;
	public const int STATE_EAT=0;
	public const int STATE_WANDER=1;
	public const int STATE_ATTACK=2;
	
	public float reacttime;//interval of reacting
	private float duringtime;
	
	private float puttime;//interval of putting bombs
	private float duringtime2;

	private float neardistance;
	
	public float speed;
	private Vector3 direction;
	private Vector3 lastposition;
	private GameObject targetitem;
	
	public float wandertime;
	private float nowwandertime;

	public float ignoretime;
	private float nowignoretime;

	public float escapetime;
	private float nowescapetime;
	
	public float sensitivity;//more likely to turn round
	public float intell;//more likely to make smart decision
	public float aggressive;//more likely to attack 
	
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
		State=STATE_WANDER;
		
		puttime=1.0f;
		duringtime=0;
		duringtime2=0;

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
		nowignoretime-=Time.deltaTime;
		nowwandertime-=Time.deltaTime;
		nowescapetime-=Time.deltaTime;

		//make a decision
		duringtime+=Time.deltaTime;
		duringtime2+=Time.deltaTime;
		if(duringtime>reacttime)
		{
			Debug.Log(State);
			puttime=(3.0f/(putbomb.getMaxBomb()+1.0f));
			neardistance=1.5f*(speed);
			switch(State)
			{
			case STATE_EAT:
				if(nowignoretime<=0)
				{
					rotate (targetitem);
				}
				if(targetitem==null)
				{
					State=STATE_WANDER;
					rotate(null);
					break;
				}
				else if(targetitem.renderer.enabled==false)
				{
					State=STATE_WANDER;
					rotate(null);
					break;
				}
				if(Vector3.Distance(transform.position,lastposition)<sensitivity*speed*Time.deltaTime)
				{
					nowignoretime=ignoretime;
					rotate(null);
					wantputbomb();
				}
				else if(indanger())
				{
					rotate(null);
				}
				break;
			case STATE_WANDER:				
				targetitem=finditem();
				if(targetitem!=null)
				{
					State=STATE_EAT;
					rotate (targetitem);
					break;
				}
				if (Random.Range(0.0f,1.0f)<aggressive/5&&nowescapetime<=0)
				{
					State=STATE_ATTACK;
					rotate(player);
					break;
				}
				if(Vector3.Distance(transform.position,lastposition)<sensitivity*speed*Time.deltaTime||nowwandertime<=0)
				{
					nowwandertime=wandertime;
					rotate(null);
					wantputbomb();
				}
				else if(indanger())
				{
					nowwandertime=wandertime;
					rotate(null);
				}
				break;
			case STATE_ATTACK:
				if(putbomb.bombenable()==false||Vector3.Distance(transform.position,lastposition)<sensitivity*speed*Time.deltaTime)
				{
					nowescapetime=escapetime;
					State=STATE_WANDER;
					rotate(null);
					break;
				}
				if (nearplayer())
				{
					Debug.Log("ss");
					wantputbomb();
				}
				break;
			}
			duringtime=0.0f;
		}
		
		//move
		direction=transform.forward;
		lastposition=transform.position;
		controller.Move(direction*Time.deltaTime*speed);
		
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
		if(indanger2("bomb"))
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
	private void rotate(GameObject temp)
	{
		if(temp==null)
		{
			if (Random.Range(0.0f,1.0f)<intell)
			{
				transform.Rotate(0.0f,180.0f,0.0f);
			}
			else{
				transform.Rotate(0.0f,Random.Range(0.0f,360.0f),0.0f);
			}
		}
		else
		{
			Vector3 position=new Vector3(temp.transform.position.x,transform.position.y,temp.transform.position.z);
			transform.LookAt(position);
		}

	}
	
	
}
