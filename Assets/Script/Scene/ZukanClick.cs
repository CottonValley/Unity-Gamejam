using UnityEngine;
using System.Collections;

public class ZukanClick : MonoBehaviour {

	void OnGUI(){
		// プレハブを取得
		//GUI.Label(new Rect(10,10,100,100), "MenuWindow");
		var prefab = Resources.Load("Prefabs/ModalZukan");
		// プレハブからインスタンスを生成
		var obj = Instantiate(prefab,new Vector3(0, 0, 0), Quaternion.identity);
		obj.name = prefab.name;
	}

}
