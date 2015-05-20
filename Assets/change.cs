using UnityEngine;
using System.Collections;
//standard

public class change : MonoBehaviour {
	//change bomb's light
	Color colorstart=Color.red;
	Color colorend=Color.green;
	public float duration;
	float t;

	void Start () {
		t=Time.time;
	}

	void Update () {
		float lerp=Mathf.PingPong(Time.time-t,duration)/duration;
		light.color=Color.Lerp(colorstart,colorend,lerp);	
	}
}
