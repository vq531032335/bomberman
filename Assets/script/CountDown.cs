using UnityEngine;
using System.Collections;
//standard

public class CountDown : MonoBehaviour {
	public UILabel countDownLabel;
	public float levelTime;

	void Start () {
		GameDataManager GDM=(GameDataManager)gameObject.GetComponent("GameDataManager");
		levelTime=GDM.gameData.leveltime;
	}

	void FixedUpdate () {
		levelTime -= Time.deltaTime;
	}

	void Update () {
		countDownLabel.text = getTime (levelTime);
	}

	string getTime(float time)
	{
		if(time<0)
		{
			return "00:00:00";
		}
		
		string lastTime = "";

		var hour = Mathf.FloorToInt(time/3600%24);	
		/*if(hour/10 >=1)
		{
			lastTime+="" + hour;
		}
		else
		{
			lastTime +="0" + hour;
		}*/
		
		var minute = Mathf.FloorToInt(time/60%60);
		if(minute/10 >=1)
		{
			lastTime+="" + minute;
		}
		else{
			lastTime +="0" + minute;
		}
		
		var second = Mathf.FloorToInt(time%60);
		if(second/10 >=1)
		{
			lastTime+=":" + second;
		}
		else
		{
			lastTime +=":0" + second;
		}

		return lastTime;
	}
}
