using UnityEngine;
using System.Collections;
//standard

public class randomrotate : MonoBehaviour {

	void Start () {
		transform.Rotate(Random.Range(0.0f,360.0f),Random.Range(0.0f,360.0f),Random.Range(0.0f,360.0f));
		Vector3 position=transform.position;
		position.y=Random.Range(0.4f,1.4f);
		transform.position=position;

	}
	
	void Update () {
	
	}
}
