using UnityEngine;
using System.Collections;
//standard

public class crasherAI : MonoBehaviour {	
	public float waittime;
	public float attacktime;
	private float nowtime;

	private NavMeshAgent navmesh;
	private GameObject target;
	private bool death;

	void Awake () {
		nowtime=0;
		navmesh=gameObject.GetComponent<NavMeshAgent>();
		target=GameObject.FindGameObjectWithTag("Player");
		death=false;
	}

	void Update () {
		//change status
		nowtime-=Time.deltaTime;
		if(nowtime<=0)
		{
			nowtime=attacktime+waittime;
		}
		if(nowtime<=attacktime)
		{
			//move
			if (death==false)
			{
				navmesh.SetDestination(target.transform.position);
			}			
			//crash
			if (Vector3.Distance(transform.position,target.transform.position)<0.8&&death==false)
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
