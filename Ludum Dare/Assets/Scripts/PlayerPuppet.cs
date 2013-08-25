using UnityEngine;
using System.Collections;

public class PlayerPuppet : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(Vector3.forward);
	}
}
