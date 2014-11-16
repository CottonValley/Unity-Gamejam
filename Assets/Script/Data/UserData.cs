using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserData {
	public int CHARACTER_NUM = 6;

	public string keyLevel = "level";
	public string keyExp = "exp";
	public string keyKilled = "killed_";

	public int level=0;
	public int exp =0;

	public List<Killed> killed = new List<Killed>();

	public struct Killed{
		public int id;
		public int num;
	}

	private static UserData __instance = null;
	public static UserData instance {
		get{
			if(__instance == null){
				__instance = new UserData();
			}
			return __instance;
		}
		private set{
			__instance = value;
		}
	}

	private UserData(){
		Load ();
	}
	
	public void Kill(Character killed){
		AddKilled(killed.id);
		AddExp(killed.get_exp);
		CheckLevelUp();
	}

	void AddKilled(int id){
		var index = killed.FindIndex( k => k.id == id);
		if(index < 0){
			var temp = new Killed();
			temp.id = id;
			temp.num = 1;

			killed.Add(temp);
			return;
		}
		else{
			var temp = killed[index];
			temp.num += 1;
			killed[index] = temp;
		}

		Debug.Log("Killed == id : "+id+"  num : "+killed[index].num );
	}

	void AddExp(int get_exp){
		exp += get_exp;
		Debug.Log("Now Exp : "+exp);
	}

	void CheckLevelUp(){
		GameData gameData = GameData.instance;
		if(gameData.stages[level].exp <= exp &&
		   level+1 < gameData.stages.Length){
			Debug.Log ("Level Up !");
			level++;
		}
		else if( level+1 >= gameData.stages.Length){
			exp = gameData.stages[level].exp;
		}
	}

	public void Save(){
		PlayerPrefs.SetInt(keyLevel , level);
		PlayerPrefs.SetInt(keyExp, exp);

		foreach(var kill in killed){
			PlayerPrefs.SetInt(keyKilled+kill.id, kill.num);
		}
	}

	public void Load(){
		level = PlayerPrefs.GetInt(keyLevel, 0);
		exp = PlayerPrefs.GetInt(keyExp, 0);

		for(int i=0; i<CHARACTER_NUM; i++){
			var kill = new Killed();
			kill.id = i;
			kill.num = PlayerPrefs.GetInt(keyKilled+ i, 0);

			if(killed.Contains(kill)){
				killed.Remove(kill);
			}
			killed.Add(kill);
		}

		Debug.Log ("User Data Load === lv : "+level+"  exp : "+exp);
	}

	public void DeleteData(){
		PlayerPrefs.DeleteAll();
	}
}
