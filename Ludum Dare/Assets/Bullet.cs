using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	int colorIndex;
	public float power;
	
	// Use this for initialization
	void Start () {
		rigidbody.AddRelativeForce(Vector3.forward*power, ForceMode.VelocityChange);
	}
	
	void OnTriggerEnter(Collider col){
		Destroy(this.gameObject);
	}

	public int ColorIndex {
		get {
			return this.colorIndex;
		}
		set {
			renderer.sharedMaterial = GameManager.Instance.colors[value];
			colorIndex = value;
		}
	}
}
