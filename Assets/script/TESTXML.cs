using UnityEngine;
using System.Collections;

public class TESTXML : MonoBehaviour {
	public GameObject Player;
	public GameObject smoker;
	public GameObject crasher;
	public GameObject cruiser;
	public GameObject stone;
	public GameObject ironball;


	// Use this for initialization
	void Start () {
		GameDataManager GDM=(GameDataManager)gameObject.GetComponent("GameDataManager");
		while(GDM.gameData.read==0){
		}
		Instantiate(Player,GDM.gameData.PlayerPosition,Player.transform.rotation);
		for(int i=0;i<GDM.gameData.smokerNum;i++)
		{
			Instantiate(smoker,GDM.gameData.smokerPosition[i],smoker.transform.rotation);
			smokerAI ai=(smokerAI)smoker.GetComponent("smokerAI");
			ai.setPoint(GDM.gameData.smokerPoint1[i],GDM.gameData.smokerPoint2[i]);
		}
		for(int i=0;i<GDM.gameData.crasherNum;i++)
		{
			Instantiate(crasher,GDM.gameData.crasherPosition[i],crasher.transform.rotation);
		}
		for(int i=0;i<GDM.gameData.cruiserNum;i++)
		{

			Instantiate(cruiser,GDM.gameData.cruiserPosition[i],cruiser.transform.rotation);
			cruiserAI ai=(cruiserAI)cruiser.GetComponent("cruiserAI");
			ai.difficult=GDM.gameData.cruiserDifficult[i];
		}
		
		for(int i=0;i<GDM.gameData.stoneNum;i++)
		{
			Instantiate(stone,GDM.gameData.stonePosition[i],stone.transform.rotation);
		}
		for(int i=0;i<GDM.gameData.ironballNum;i++)
		{
			Instantiate(ironball,GDM.gameData.ironballPosition[i],ironball.transform.rotation);
		}
		



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
