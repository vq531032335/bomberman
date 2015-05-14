using UnityEngine;
using System.Collections;

public class change : MonoBehaviour {

	Color colorstart=Color.red;
	Color colorend=Color.green;
	public float duration;
	float t;
	// Use this for initialization
	void Start () {
		t=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float lerp=Mathf.PingPong(Time.time-t,duration)/duration;
		light.color=Color.Lerp(colorstart,colorend,lerp);
	
	}


}
