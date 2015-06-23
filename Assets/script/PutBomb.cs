using UnityEngine;
using System.Collections;
//standard

public class PutBomb : MonoBehaviour {
	public GameObject bombfabs;
	public int Maxbomb;
	private int nowbomb;
	public float power;
	public int life;
	public int maxLife;
	public float lefttime;

	public float invincibletime;
	private float nowinvincibletime;

	private UISprite[] blood=new UISprite[10];
	private UISprite[] bloodPic=new UISprite[10];

	private UISprite[] bomb=new UISprite[10];
	private UISprite[] bombPic=new UISprite[10];

	private UILabel bombpower;
	private UILabel walkspeed;

	void Awake()
	{
		if (tag=="Player")
		{
			for(int i=0;i<10;i++)
			{
				blood[i] = GameObject.Find ("Blood"+(i+1).ToString()).GetComponent<UISprite> ();
				bloodPic[i] = GameObject.Find ("BloodPic"+(i+1).ToString()).GetComponent<UISprite> ();
				bomb[i] = GameObject.Find ("Bomb"+(i+1).ToString()).GetComponent<UISprite> ();
				bombPic[i] = GameObject.Find ("BombPic"+(i+1).ToString()).GetComponent<UISprite> ();
			}
			bombpower = GameObject.Find ("powerlabel").GetComponent<UILabel>();
			walkspeed = GameObject.Find ("speedlabel").GetComponent<UILabel>();
			for(int i=0;i!=maxLife;i++)
			{
				blood[i].alpha=1;
			}
		}
	}

	void Start () 
	{
		nowbomb=0;
		nowinvincibletime=0;
	}
	
	void Update () 
	{
		nowinvincibletime-=Time.deltaTime;

		if (tag=="Player")
		{
			//处理炸弹条
			for(int i=0;i!=nowbomb;i++)
			{
				bombPic[i].alpha=0;
			}
			for(int i=nowbomb;i!=10;i++)
			{
				bombPic[i].alpha=1;
			}
			
			for(int i=0;i!=Maxbomb;i++)
			{
				bomb[i].alpha=1;
			}
			for(int i=Maxbomb;i!=10;i++)
			{
				bomb[i].alpha=0;
			}
			
			//处理血条
			for(int i=0;i!=life;i++)
			{
				bloodPic[i].alpha=1;
			}
			for(int i=Mathf.Min(life,10);i!=10;i++)
			{
				bloodPic[i].alpha=0;
			}

			bombpower.text="bomb power:"+power;
			ThirdPersonController TPC=(ThirdPersonController)gameObject.GetComponent("ThirdPersonController");
			walkspeed.text="Speed:"+ TPC.GetSpeed();

		}
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
		if(Maxbomb!=10)
		{			
			Maxbomb++;
		}
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
