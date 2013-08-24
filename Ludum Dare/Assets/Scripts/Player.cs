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
	
	// Use this for initialization
	void Start () {
		
		characters = new GameObject[4];
		
		for (int i = 0; i < 4; i++) {
			characters[i] = Instantiate (characterPrefab) as GameObject;
			Vector3 pos = new Vector3();
			switch(i){
			case 0 :
				pos = new Vector3(
					transform.position.x - distanceFromCenter,
					transform.position.y,
					transform.position.z);
				break;
			case 1 :
				pos = new Vector3(
					transform.position.x,
					transform.position.y,
					transform.position.z + distanceFromCenter);
				break;
			case 2 :
				pos = new Vector3(
					transform.position.x + distanceFromCenter,
					transform.position.y,
					transform.position.z);
				break;
			case 3 :
				pos = new Vector3(
					transform.position.x,
					transform.position.y,
					transform.position.z - distanceFromCenter);
				break;
			}
			characters[i].transform.position = pos;
			characters[i].transform.parent = this.transform;
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
	}
}
