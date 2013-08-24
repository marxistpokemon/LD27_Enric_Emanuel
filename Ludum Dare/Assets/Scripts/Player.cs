using UnityEngine;
using System.Collections;

public enum PlayerChar {
	HEALER,
	WIZARD,
	WARRIOR,
	ARCHER
}

public class Player : MonoBehaviour {
	
	public GameObject characterPrefab;
	public GameObject[] characters;
	public float distanceFromCenter;
	
	public Color wizardColor;
	public Color healerColor;
	public Color warriorColor;
	public Color archerColor;
	
	public GoTweenConfig goConfig;
	
	public float rotationTime = 0.5f;
	public float speed;
	
	public Vector3 playerInput;
	
	// Use this for initialization
	void Start () {
		
		characters = new GameObject[4];
		
		for (int i = 0; i < 4; i++) {
			characters[i] = Instantiate (characterPrefab) as GameObject;
			characters[i].transform.parent = this.transform;
			Vector3 pos = new Vector3();
			switch(i){
			case (int)PlayerChar.ARCHER :
				pos = new Vector3(0, 0, -distanceFromCenter);
				break;
			case (int)PlayerChar.WIZARD :
				pos = new Vector3(0, 0, distanceFromCenter);
				break;
			case (int)PlayerChar.HEALER :
				pos = new Vector3(-distanceFromCenter, 0, 0);
				break;
			case (int)PlayerChar.WARRIOR :
				pos = new Vector3(distanceFromCenter, 0, 0);
				break;
			}
			characters[i].transform.localPosition = pos;
		}
		
		characters[(int)PlayerChar.ARCHER].renderer.material.color = archerColor;
		characters[(int)PlayerChar.WIZARD].renderer.material.color = wizardColor;
		characters[(int)PlayerChar.WARRIOR].renderer.material.color = warriorColor;
		characters[(int)PlayerChar.HEALER].renderer.material.color = healerColor;
		
		goConfig = new GoTweenConfig();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("XButtonA")){
			Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,270,0))
				.setEaseType(GoEaseType.QuadInOut));
		}
		if(Input.GetButtonDown("XButtonB")){
			Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,0,0))
				.setEaseType(GoEaseType.QuadInOut));
		}
		if(Input.GetButtonDown("XButtonY")){
			Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,90,0))
				.setEaseType(GoEaseType.QuadInOut));
		}
		if(Input.GetButtonDown("XButtonX")){
			Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,180,0))
				.setEaseType(GoEaseType.QuadInOut));
		}
		
		playerInput = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
		playerInput.Normalize();
	}
	
	void FixedUpdate(){
		rigidbody.MovePosition(playerInput*Time.fixedDeltaTime*speed+transform.position);
	}
}
