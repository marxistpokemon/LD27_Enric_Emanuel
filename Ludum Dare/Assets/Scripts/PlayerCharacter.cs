using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(Vector3.forward);
	}
}
