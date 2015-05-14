using UnityEngine;
using System.Collections;

public class death : MonoBehaviour {
	private GameObject player;
	private GameObject enemy;
	public GameObject bombfabs;
	private float nowtime;
	// Use this for initialization
	void Start () {
		player=GameObject.Find("aircraft");
		enemy=GameObject.Find("enemy");
		nowtime=3.0f;

		
		Destroy(enemy.GetComponent("AI"));
	
	}
	
	// Update is called once per frame
	void Update () {
		nowtime-=Time.deltaTime;
		if(nowtime>0&&bombfabs!=null)
		{
			Vector3 position=new Vector3(transform.position.x+Random.Range(-0.3f,0.3f),Random.Range(0.0f,0.6f),transform.position.z+Random.Range(-0.3f,0.3f));
			GameObject bomb=(GameObject)Instantiate(bombfabs,position,transform.rotation);
			boommm bbb=(boommm)bomb.GetComponent("boommm");
			bbb.setpower(0.1f);
			bbb.setlefttime(0.01f);
		}

	}
}
