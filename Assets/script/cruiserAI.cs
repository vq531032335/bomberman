using UnityEngine;
using System.Collections;
//standard

public class cruiserAI : MonoBehaviour {	
	private CharacterController controller;
	private GameObject player;
	private PutBomb putbomb;
	
	private int State;//state changing
	public const int STATE_EAT=0;
	public const int STATE_WANDER=1;
	public const int STATE_ATTACK=2;
	
	public float reacttime;//interval of reacting
	private float duringtime;
	
	private float puttime;//interval of putting bombs
	private float duringtime2;

	private float neardistance;
	
	public float speed;//controll movement
	private Vector3 direction;
	private Vector3 lastposition;
	private GameObject targetitem;
	
	public float wandertime;//interval of change direction
	private float nowwandertime;

	public float ignoretime;//time of ignore item
	private float nowignoretime;

	public float escapetime;//time of escape from bomb
	private float nowescapetime;
	
	public float sensitivity;//more likely to turn round
	public float intell;//more likely to make reverse decision
	public float aggressive;//more likely to attack 

	public int difficult;
	private bool diff;
	
	void Awake () {
		controller=(CharacterController)gameObject.GetComponent("CharacterController");
		player=GameObject.FindGameObjectWithTag("Player");		
		putbomb=(PutBomb)gameObject.GetComponent("PutBomb");

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

		diff=false;
	}
	
	void Update () {
		if(diff==false)
		{
			if(difficult!=0)
			{
				setDifficult();
				diff=true;
			}
		}
		if (renderer.enabled==true)
		{			
			nowignoretime-=Time.deltaTime;
			nowwandertime-=Time.deltaTime;
			nowescapetime-=Time.deltaTime;
			duringtime+=Time.deltaTime;
			duringtime2+=Time.deltaTime;

			if(duringtime>reacttime)
			{
				//make a decision
				puttime=(3.0f/(putbomb.getMaxBomb()+1.0f));
				neardistance=0.5f*(speed+putbomb.getMaxBomb());
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
					break;
				}
				duringtime=0.0f;
			}

			//put bomb
			if(State==STATE_ATTACK)
			{
				if (nearplayer())
				{
					wantputbomb();
				}
			}
			
			//move
			direction=transform.forward;
			lastposition=transform.position;
			Vector3 fixposition=new Vector3(lastposition.x, 0.8f,lastposition.z);
			transform.position=fixposition;
			controller.Move(direction*Time.deltaTime*speed);
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

		PutBomb putbomb=(PutBomb)gameObject.GetComponent("PutBomb");
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
		if (mindistance<(speed+1.0f)*4.0f)
		{
			ThirdPersonController TPC=(ThirdPersonController)player.GetComponent("ThirdPersonController");
			float temp=TPC.GetSpeed();
			if(Vector3.Distance(minitem.transform.position,player.transform.position)*speed/temp>mindistance)
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
	public void setDifficult()
	{
		switch(difficult)
		{
		case 1:
		{
			putbomb.power=1.0f;
			putbomb.Maxbomb=1;
			putbomb.life=12;
			reacttime=0.4f;
			speed=1.0f;
			sensitivity=0.4f;
			
			break;
		}
		case 2:
		{
			putbomb.power=3.0f;
			putbomb.Maxbomb=3;
			putbomb.life=15;
			reacttime=0.3f;
			speed=3.0f;
			sensitivity=0.6f;
			
			break;
		}
		case 3:
		{
			putbomb.power=7.0f;
			putbomb.Maxbomb=10;
			putbomb.life=30;
			reacttime=0.2f;
			speed=4.0f;
			sensitivity=0.7f;
			
			break;
		}
		}
	}
	
}
