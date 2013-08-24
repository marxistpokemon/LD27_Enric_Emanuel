using UnityEngine;
using System.Collections;

public enum PlayerChar {
	HEALER,
	WIZARD,
	WARRIOR,
	ARCHER
}

[System.Serializable]
public class Coord {
	
	public int r;
	public int c;
	
	public Coord (int pR, int pC){
		r = pR;
		c = pC;
	}
	
	public override bool Equals (object obj)
	{
		Coord queryCoord = obj as Coord;
		return (r == queryCoord.r && c == queryCoord.c);
	}
}

public class Player : MonoBehaviour {
	
	public Coord gridPos;
	
	public GameObject characterPrefab;
	public GameObject[] characters;
	public float distanceFromCenter;
	
	public Color wizardColor;
	public Color healerColor;
	public Color warriorColor;
	public Color archerColor;
	
	public GoTweenConfig goConfig;
	
	public float rotationTime = 0.5f;
	public float moveDuration = 0.5f;
	public float speed;
	
	public Vector3 input;
	public bool isMoving = false;
	
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
		
	}
	
	
	
	void FixedUpdate(){
		//rigidbody.MovePosition(input*Time.fixedDeltaTime*speed+transform.position);
	}
}
