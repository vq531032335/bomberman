using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {
	public UILabel countDownLabel;
	public float Time1;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		Time1 -= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		countDownLabel.text = getTime (Time1);
	}

	string getTime(float time)
	{
		if(time<0)
		{
			return "00:00:00";
		}
		
		string lastTime = "";

		var hour = Mathf.FloorToInt(time/3600%24);	
		if(hour/10 >=1)
		{
			lastTime+="" + hour;
		}
		else
		{
			lastTime +="0" + hour;
		}
		
		var minute = Mathf.FloorToInt(time/60%60);
		if(minute/10 >=1)
		{
			lastTime+=":" + minute;
		}
		else{
			lastTime +=":0" + minute;
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
