using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Killer : MonoBehaviour {
	public int id = 0;
	public int appear_lv = 0;
	public string killer_name = "";
	public string discription = "";

	public List<GameObject> characters;
	private GameObject target = null;
	private bool moving = false;
	private Vector2 toPos;
	private float moveTime = 1f;
	private float movingTimer = 0f;

	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!moving && characters.Count > 0){
			moving = true;
			target = characters.First();
			var tempP = target.transform.position;
			toPos =  new Vector2( transform.position.x-tempP.x, transform.position.y-tempP.y );
			characters.Remove(target);
		}
	}

	void FixedUpdate() {
		if(target != null){
			movingTimer += Time.deltaTime;
			var nowPos = new Vector2(transform.position.x, transform.position.y);
			rigidbody2D.MovePosition( nowPos - toPos*Time.deltaTime);

			var percent = movingTimer/moveTime;
			if(percent >= 1){
				movingTimer = 0;
				moving = false;

				UserData userData = UserData.instance;
				userData.Kill(target.transform.GetComponent<Character>());

				Destroy(target);
				target = null;
			}
		}
	}
}
