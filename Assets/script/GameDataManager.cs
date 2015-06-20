//=========================================================================================================
//Note: Data Managing.
//Date Created: 2012/04/17 by 风宇冲
//Date Modified: 2012/12/14 by 风宇冲
//=========================================================================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;


//GameData,储存数据的类，把需要储存的数据定义在GameData之内就行//
public class GameData
{


	//密钥,用于防止拷贝存档//
	public string key;
	
	//下面是添加需要储存的内容//
	public int read;
	public float leveltime;

	public Vector3 PlayerPosition;

	public int smokerNum;
	public Vector3[] smokerPosition;
	public Vector3[] smokerPoint1;
	public Vector3[] smokerPoint2;

	public int crasherNum;
	public Vector3[] crasherPosition;

	public int cruiserNum;
	public Vector3[] cruiserPosition;
	public int[] cruiserDifficult;

	public int stoneNum;
	public Vector3[] stonePosition;

	public int ironballNum;
	public Vector3[] ironballPosition;

	public GameData()
	{

		if(GameObject.Find("Space").renderer.enabled)//制作关卡,true为编辑模式
		{
			read=1;
			leveltime=300.0f;
			PlayerPosition = new Vector3(-8.0f,1.4f,-8.0f);
			
			smokerNum=0;
			
			crasherNum=0;
			
			cruiserNum=3;
			cruiserPosition=new Vector3[cruiserNum];
			cruiserDifficult=new int[cruiserNum];
			cruiserPosition[0]=new Vector3(8.0f,2.0f,-8.0f);
			cruiserDifficult[0]=2;
			cruiserPosition[1]=new Vector3(-8.0f,2.0f,8.0f);
			cruiserDifficult[1]=2;
			cruiserPosition[2]=new Vector3(8.0f,2.0f,8.0f);
			cruiserDifficult[2]=2;

			//用来编辑石头和铁块
			GameObject[] ddd=GameObject.FindGameObjectsWithTag("stone");
			stoneNum=ddd.Length;
			stonePosition=new Vector3[stoneNum];
			int q=0;
			foreach(GameObject pp in ddd)
			{
				stonePosition[q]=pp.transform.position;
				q++;
			}
			GameObject[] eee=GameObject.FindGameObjectsWithTag("ironball");
			ironballNum=eee.Length;
			ironballPosition=new Vector3[ironballNum];
			int p=0;
			foreach(GameObject pp in eee)
			{
				ironballPosition[p]=pp.transform.position;
				p++;
			}
		}
		 
	}
}
//管理数据储存的类//
public class GameDataManager:MonoBehaviour
{
	public static int level=3;
	public static string dataFileName ="level3.dat";
	//存档文件的名称,自己定//
	private  XmlSaver xs = new XmlSaver();
	
	public  GameData gameData;
	
	public void Awake()
	{
		gameData = new GameData();
		
		//设定密钥，根据具体平台设定//
		gameData.key = SystemInfo.deviceUniqueIdentifier;
		Load();
	}
	
	//存档时调用的函数//
	public  void Save()
	{
		string gameDataFile = GetDataPath() + "/"+dataFileName;
		string dataString= xs.SerializeObject(gameData,typeof(GameData));
		xs.CreateXML(gameDataFile,dataString);
	}
	
	//读档时调用的函数//
	public  void Load()
	{
		string gameDataFile = GetDataPath() + "/"+dataFileName;
		if(xs.hasFile(gameDataFile))
		{
			string dataString = xs.LoadXML(gameDataFile);
			GameData gameDataFromXML = xs.DeserializeObject(dataString,typeof(GameData)) as GameData;
			
			//是合法存档//
			if(gameDataFromXML.key == gameData.key)
			{
				gameData = gameDataFromXML;
			}
			//是非法拷贝存档//
			else
			{
				//留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
			}
		}
		else
		{
			if(gameData != null)
				Save();
			Debug.Log("save successfully!");
		}
	}
	
	//获取路径//
	private static string GetDataPath()
	{
		// Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents
		// Application.dataPath returns ar/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data             
		// Strip "/Data" from path
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5);
			// Strip application name
			path = path.Substring(0, path.LastIndexOf('/')); 
			return path + "/Documents";
		}
		else
			//    return Application.dataPath + "/Resources";
			return Application.dataPath;
	}
}