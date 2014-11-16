using UnityEngine;
using System.Collections;

public class ModalZukan : MonoBehaviour {
	public GameObject PrefabX;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		Instantiate(this.PrefabX, new Vector3(0, 0, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
