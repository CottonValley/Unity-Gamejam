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

	public void AddKilled(int id){
		if(id >= killed.Length) return;

		var temp = killed[id];
		temp.num = temp.num+1;

		killed[id] = temp;
	}
}
