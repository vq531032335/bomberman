using UnityEngine;
using System.Collections;

public class randomrotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Rotate(Random.Range(0.0f,360.0f),Random.Range(0.0f,360.0f),Random.Range(0.0f,360.0f));
		Vector3 position=transform.position;
		position.y=Random.Range(0.4f,1.6f);
		transform.position=position;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
