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
//	public int startLife;
	public float lefttime;

	public float invincibletime;
	private float nowinvincibletime;

//	private UISprite sprite1;
//	private UISprite sprite2;
//	private UISprite sprite3;
//	private UISprite sprite4;
//	private UISprite sprite5;

	private UISprite[] blood=new UISprite[10];
	private UISprite[] bloodPic=new UISprite[10];

	private UISprite[] bomb=new UISprite[10];
	private UISprite[] bombPic=new UISprite[10];

	void Awake()
	{
//		sprite1 = GameObject.Find ("BombPic1").GetComponent<UISprite> ();
//		sprite2 = GameObject.Find ("BombPic2").GetComponent<UISprite> ();
//		sprite3 = GameObject.Find ("BombPic3").GetComponent<UISprite> ();
//		sprite4 = GameObject.Find ("BombPic4").GetComponent<UISprite> ();
//		sprite5 = GameObject.Find ("BombPic5").GetComponent<UISprite> ();

		//获取血条
		blood[0] = GameObject.Find ("Blood1").GetComponent<UISprite> ();
		blood[1] = GameObject.Find ("Blood2").GetComponent<UISprite> ();
		blood[2] = GameObject.Find ("Blood3").GetComponent<UISprite> ();
		blood[3] = GameObject.Find ("Blood4").GetComponent<UISprite> ();
		blood[4] = GameObject.Find ("Blood5").GetComponent<UISprite> ();
		blood[5] = GameObject.Find ("Blood6").GetComponent<UISprite> ();
		blood[6] = GameObject.Find ("Blood7").GetComponent<UISprite> ();
		blood[7] = GameObject.Find ("Blood8").GetComponent<UISprite> ();
		blood[8] = GameObject.Find ("Blood9").GetComponent<UISprite> ();
		blood[9] = GameObject.Find ("Blood10").GetComponent<UISprite> ();

		bloodPic[0] = GameObject.Find ("BloodPic1").GetComponent<UISprite> ();
		bloodPic[1] = GameObject.Find ("BloodPic2").GetComponent<UISprite> ();
		bloodPic[2] = GameObject.Find ("BloodPic3").GetComponent<UISprite> ();
		bloodPic[3] = GameObject.Find ("BloodPic4").GetComponent<UISprite> ();
		bloodPic[4] = GameObject.Find ("BloodPic5").GetComponent<UISprite> ();
		bloodPic[5] = GameObject.Find ("BloodPic6").GetComponent<UISprite> ();
		bloodPic[6] = GameObject.Find ("BloodPic7").GetComponent<UISprite> ();
		bloodPic[7] = GameObject.Find ("BloodPic8").GetComponent<UISprite> ();
		bloodPic[8] = GameObject.Find ("BloodPic9").GetComponent<UISprite> ();
		bloodPic[9] = GameObject.Find ("BloodPic10").GetComponent<UISprite> ();

		//获取炸弹
		bomb[0] = GameObject.Find ("Bomb1").GetComponent<UISprite> ();
		bomb[1] = GameObject.Find ("Bomb2").GetComponent<UISprite> ();
		bomb[2] = GameObject.Find ("Bomb3").GetComponent<UISprite> ();
		bomb[3] = GameObject.Find ("Bomb4").GetComponent<UISprite> ();
		bomb[4] = GameObject.Find ("Bomb5").GetComponent<UISprite> ();
		bomb[5] = GameObject.Find ("Bomb6").GetComponent<UISprite> ();
		bomb[6] = GameObject.Find ("Bomb7").GetComponent<UISprite> ();
		bomb[7] = GameObject.Find ("Bomb8").GetComponent<UISprite> ();
		bomb[8] = GameObject.Find ("Bomb9").GetComponent<UISprite> ();
		bomb[9] = GameObject.Find ("Bomb10").GetComponent<UISprite> ();
		
		bombPic[0] = GameObject.Find ("BombPic1").GetComponent<UISprite> ();
		bombPic[1] = GameObject.Find ("BombPic2").GetComponent<UISprite> ();
		bombPic[2] = GameObject.Find ("BombPic3").GetComponent<UISprite> ();
		bombPic[3] = GameObject.Find ("BombPic4").GetComponent<UISprite> ();
		bombPic[4] = GameObject.Find ("BombPic5").GetComponent<UISprite> ();
		bombPic[5] = GameObject.Find ("BombPic6").GetComponent<UISprite> ();
		bombPic[6] = GameObject.Find ("BombPic7").GetComponent<UISprite> ();
		bombPic[7] = GameObject.Find ("BombPic8").GetComponent<UISprite> ();
		bombPic[8] = GameObject.Find ("BombPic9").GetComponent<UISprite> ();
		bombPic[9] = GameObject.Find ("BombPic10").GetComponent<UISprite> ();
	}

	void Start () 
	{
		nowbomb=0;
		nowinvincibletime=0;
	}
	
	void Update () 
	{
		nowinvincibletime-=Time.deltaTime;

		//处理炸弹条
		for(int i=0;i!=nowbomb;i++)
		{
			bombPic[i].alpha=1;
		}
		for(int i=nowbomb;i!=10;i++)
		{
			bombPic[i].alpha=0;
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
		for(int i=life;i!=10;i++)
		{
			bloodPic[i].alpha=0;
		}
		
		for(int i=0;i!=maxLife;i++)
		{
			blood[i].alpha=1;
		}
		for(int i=maxLife;i!=10;i++)
		{
			blood[i].alpha=0;
		}

		if(Input.GetKeyDown(KeyCode.Space)&& tag=="Player")
		{
//			sprite.fillAmount = 0;
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
