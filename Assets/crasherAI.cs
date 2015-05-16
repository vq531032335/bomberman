using UnityEngine;
using System.Collections;

public class crasherAI : MonoBehaviour {	
	private NavMeshAgent navmesh;
	private GameObject target;
	private bool death;
	// Use this for initialization
	void Start () {
		navmesh=gameObject.GetComponent<NavMeshAgent>();
		target=GameObject.FindGameObjectWithTag("Player");
		death=false;
	}
	
	// Update is called once per frame
	void Update () {			
		if (renderer.enabled==true && death==false)
		{
			navmesh.SetDestination(target.transform.position);
			if (Vector3.Distance(transform.position,target.transform.position)<0.8)
			{
				PutBomb pp=(PutBomb)target.GetComponent("PutBomb");
				pp.minuslife();
				die();
			}
		}
	}
	public void die()
	{
		death=true;
		collider.enabled=false;
		audio.Play();
		particleSystem.Play();
		this.renderer.enabled=false;
		Destroy(this.gameObject,4);
	}
}
