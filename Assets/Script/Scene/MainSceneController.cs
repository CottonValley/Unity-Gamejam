using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MainSceneController : MonoBehaviour {
	public GameObject[] base_chara_obj;
	public float appearTime = 40f;
	public float arriveTime = 5f;
	public int MaxCharacter = 10;

	public Slider expSlider;

	private List<Character> characters = new List<Character>();
	private float playingTimer = 0f;
	private float appearingTimer = 0f;
	private int appearCount = 0;
	private List<GameObject> appearSpot = new List<GameObject>();
	private bool appearing = false;

	// Use this for initialization
	void Start () {
		for(int i=1; i<=MaxCharacter; i++){
			appearSpot.Add( GameObject.Find ("Appear_"+i).gameObject );
		}

		foreach(var obj in base_chara_obj){
			characters.Add( obj.transform.GetComponent<Character>() );
		}
		
		var userData = UserData.instance;
		var gameData = GameData.instance;
		
		if(userData.level>0){
			expSlider.minValue = gameData.stages[userData.level-1].exp;
			expSlider.maxValue = gameData.stages[userData.level].exp;
			expSlider.value = userData.exp;
		}
		else{
			expSlider.minValue = 0;
			expSlider.maxValue = gameData.stages[0].exp;
			expSlider.value = userData.exp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var userData = UserData.instance;
		if(expSlider.value != userData.exp){
			AddExpSlider();
		}

		if(!appearing){
			playingTimer += Time.deltaTime;
			if(playingTimer > appearTime){
				appearing = true;
				playingTimer = 0f;

				AppearCharacter( GetRandomCharaID() );
			}
		}

		if(appearing){
			appearingTimer += Time.deltaTime;
			if(appearingTimer > arriveTime){
				appearingTimer = 0f;
				
				AppearCharacter( GetRandomCharaID() );
			}
		}

		if(appearCount >= MaxCharacter){
			appearCount = 0;
			appearing =false;
		}
	}

	void AppearCharacter(int id){
		if(base_chara_obj.Length <= id) return;
		appearSpot = appearSpot.OrderBy(i => Guid.NewGuid()).ToList();
		foreach(var spot in appearSpot){
			if(spot.transform.childCount > 0) continue;

			var obj = Instantiate(base_chara_obj[id]) as GameObject;
			obj.transform.SetParent( spot.transform );
			obj.transform.localPosition = Vector3.zero;

			break;
		}
		appearCount++;
	}

	int GetRandomCharaID(){
		UserData userData = UserData.instance;
		var appers = characters.FindAll(c => c.appear_lv <= userData.level);
		appers = appers.OrderBy(i => Guid.NewGuid()).ToList();

		var chara = appers.First();

		return chara.id;
	}
	
	void AddExpSlider(){
		expSlider.value++;
		if(expSlider.value >= expSlider.maxValue){
			var userData = UserData.instance;
			var gameData = GameData.instance;
			expSlider.minValue = gameData.stages[userData.level-1].exp;
			expSlider.maxValue = gameData.stages[userData.level].exp;
		}
	}
}
