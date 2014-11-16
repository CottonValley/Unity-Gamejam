using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Killer : MonoBehaviour {
	public int id = 0;
	public int appear_lv = 0;
	public string killer_name = "";
	public string discription = "";
	public float movePerFrame = 0.1f;

	public List<GameObject> characters;
	private GameObject target = null;
	private bool moving = false;
	private Vector2 toPos;
	private float movingTimer = 0f;

	private float walkTime = 8f;
	private float walkTimer =0f;
	private bool walking = false;
	private float walkingCounter = 0f;
	private Vector2 walkPos = Vector2.zero;

	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!moving && characters.Count > 0){
			moving = true;
			walking = false;
			walkTimer = 0;

			target = characters.First();
			var tempP = target.transform.position;
			toPos =  new Vector2( transform.position.x-tempP.x, transform.position.y-tempP.y );
			characters.Remove(target);
			return;
		}
		else if( !(moving || walking) ){
			walkTimer += Time.deltaTime;
			if(walkTimer >= walkTime){
				walking = true;
				walkTimer = 0f;
				SetWalk();
			}
		}
	}

	void FixedUpdate() {
		if(target != null){
			movingTimer += movePerFrame;
			var nowPos = new Vector2(transform.position.x, transform.position.y);
			rigidbody2D.MovePosition( nowPos - toPos*movePerFrame);
			var distance = nowPos-toPos*Time.deltaTime;
			Debug.Log (" x : "+distance.x+"   y : "+distance.y);

			if(movingTimer >= 1){
				Debug.Log (" x : "+distance.x+"   y : "+distance.y+"     toX : "+toPos.x+"   toY : "+toPos.y);
				movingTimer = 0;
				moving = false;

				UserData userData = UserData.instance;
				userData.Kill(target.transform.GetComponent<Character>());

				audio.Play();

				Destroy(target);
				target = null;
			}
		}
		else if( walking ){
			var nowPos = new Vector2(transform.position.x, transform.position.y);
			walkingCounter += 0.005f;
			rigidbody2D.MovePosition( nowPos - walkPos*0.005f);

			if(walkingCounter >= 1){
				walkingCounter = 0;
				walkPos = Vector2.zero;
				walking = false;
			}
		}
	}

	void SetWalk(){
		var id = Random.Range(1, 10);
		var obj = GameObject.Find("Appear_"+id);

		var pos = obj.transform.position;
		walkPos =  new Vector2( transform.position.x-pos.x+Random.Range(-20, 20), transform.position.y-pos.y+Random.Range(-20, 20) );
		Debug.Log ("Walk : "+id);
	}
}
