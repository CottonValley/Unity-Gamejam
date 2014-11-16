using UnityEngine;
using System.Collections;

public class UserData {
	public int CHARACTER_NUM = 6;

	public int level=0;
	public int exp =0;

	public Killed[] killed;

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
		killed = new Killed[CHARACTER_NUM];
	}
	
	public void Kill(Character killed){
		AddKilled(killed.id);
		AddExp(killed.get_exp);
		CheckLevelUp();
	}

	void AddKilled(int id){
		if(id >= killed.Length) return;

		var temp = killed[id];
		temp.num = temp.num+1;

		killed[id] = temp;
		Debug.Log("Killed == id : "+id+"  num : "+temp.num);
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
	}
}
