using UnityEngine;
using System.Collections;

public class GameData {
	private int BaseExp = 12;
	private int MaxLevel = 3;

	public struct Stage{
		public int id;
		public int exp;
	}

	public Stage[] stages;

	private static GameData __instance = null;
	public static GameData instance{
		get{
			if(__instance == null){
				__instance = new GameData();
			}
			return __instance;
		}
		private set{
			__instance = value;
		}
	}

	private GameData(){
		stages = new Stage[MaxLevel];
		for(int i=0; i<MaxLevel; i++){
			Stage temp = new Stage();
			temp.id = i;
			temp.exp = Mathf.FloorToInt(BaseExp*(Mathf.Pow(1.9f, (i+1))-1.9f+1)/0.5f);

			stages[i] = temp;
			Debug.Log ("Stage== id : "+i+"  exp : "+temp.exp);
		}
	}
}
