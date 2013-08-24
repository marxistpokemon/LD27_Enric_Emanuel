using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	
	/// <summary>
	/// The ctrl.
	/// </summary>
	CharacterController ctrl;
	
	/// <summary>
	/// The gravity.
	/// </summary>
	public Vector3 gravity = Vector3.down * 10f;
	
	// Use this for initialization
	void Start () {
		ctrl = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!ctrl.isGrounded) 
		{
			ctrl.Move(gravity * Time.deltaTime);	
			return;
		}
		
		ctrl.Move(GetDesiredVelocity() * Time.deltaTime);
	}
	
	/// <summary>
	/// Gets the desired velocity.
	/// </summary>
	/// <returns>
	/// The desired velocity.
	/// </returns>
	Vector3 GetDesiredVelocity() {
		Vector3 desiredVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		float totalMovement = Mathf.Abs(desiredVelocity.x) + Mathf.Abs(desiredVelocity.y) + Mathf.Abs(desiredVelocity.z);
		
		if(totalMovement != 0f) {
			desiredVelocity = new Vector3(desiredVelocity.x / totalMovement, 
										  desiredVelocity.y / totalMovement,
										  desiredVelocity.z / totalMovement);
		}
		
		return desiredVelocity * 10f;
	}
}
