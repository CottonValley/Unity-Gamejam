using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour {
	public int id = 0;
	public string chara_name = "";
	public int get_exp = 0;
	public string discription1 = "";
	public string discription2 = "";
	public int appear_lv = 0;
	public int need_kills = 0;

	private GameObject killer = null;
	private bool innerAwaked = false;
	private bool touched = false;

	// Use this for initialization
	void InnerAwake () {
		killer = GameObject.FindWithTag("Player").gameObject;
		if(killer == null) return;

		innerAwaked = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!innerAwaked){
			InnerAwake();
			return;
		}
	}

	public void TouchCharacter(){
		if(!innerAwaked) return;
		if(touched) return;
		touched = true;
		killer.transform.GetComponent<Killer>().characters.Add(gameObject);
	}
}
