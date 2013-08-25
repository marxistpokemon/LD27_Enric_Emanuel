using UnityEngine;
using System.Collections;

public enum PlayerChar {
	A,
	B,
	X,
	Y
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

public class PlayerCharacter : MonoBehaviour {
	
	public GameObject characterPrefab;
	public GameObject[] characters;
	public float distanceFromCenter;
	
	public GoTweenConfig goConfig;
	
	public float rotationTime = 0.5f;
	public float speed;
	
	public Bullet bulletPrefab;
	public PlayerRoot root;
	public Transform gunPoint;
	
	public PlayerChar currentCharacter;
	
	// Use this for initialization
	void Start () {
		
		characters = new GameObject[4];
		
		for (int i = 0; i < 4; i++) {
			characters[i] = Instantiate (characterPrefab) as GameObject;
			characters[i].transform.parent = this.transform;
			Vector3 pos = new Vector3();
			switch(i){
			case (int)PlayerChar.X :
				pos = new Vector3(0, 0, -distanceFromCenter);
				break;
			case (int)PlayerChar.B :
				pos = new Vector3(0, 0, distanceFromCenter);
				break;
			case (int)PlayerChar.A :
				pos = new Vector3(distanceFromCenter, 0, 0);
				break;
			case (int)PlayerChar.Y :
				pos = new Vector3(-distanceFromCenter, 0, 0);
				break;
			}
			characters[i].transform.localPosition = pos;
		}
		
		characters[(int)PlayerChar.Y].renderer.sharedMaterial = 
			GameManager.Instance.colors[(int)PlayerChar.Y];
		characters[(int)PlayerChar.B].renderer.sharedMaterial = 
			GameManager.Instance.colors[(int)PlayerChar.B];
		characters[(int)PlayerChar.A].renderer.sharedMaterial = 
			GameManager.Instance.colors[(int)PlayerChar.A];
		characters[(int)PlayerChar.X].renderer.sharedMaterial = 
			GameManager.Instance.colors[(int)PlayerChar.X];
		
		currentCharacter = PlayerChar.B;
		goConfig = new GoTweenConfig();
		
		StartCoroutine(CheckInput());
	}
	
	
	public bool canRotate = true;
	
	IEnumerator CheckInput() {
		
		while(true) {
			
			if(Input.GetButtonDown("XButtonA") && canRotate ) {
				canRotate = false;
				Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,270,0))
					.setEaseType(GoEaseType.QuadInOut))
					.setOnCompleteHandler(rotate => StartCoroutine(EndRotation(PlayerChar.A)));
			}
			if(Input.GetButtonDown("XButtonB") && canRotate ) {
				canRotate = false;
				Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,0,0))
					.setEaseType(GoEaseType.QuadInOut))
					.setOnCompleteHandler(rotate => StartCoroutine(EndRotation(PlayerChar.B)));
			}
			if(Input.GetButtonDown("XButtonY") && canRotate ) {
				canRotate = false;
				Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,90,0))
					.setEaseType(GoEaseType.QuadInOut))
					.setOnCompleteHandler(rotate => StartCoroutine(EndRotation(PlayerChar.Y)));
			}
			if(Input.GetButtonDown("XButtonX") && canRotate ) {
				canRotate = false;
				Go.to(this.transform, rotationTime, new GoTweenConfig().rotation(Quaternion.Euler(0,180,0))
					.setEaseType(GoEaseType.QuadInOut))
					.setOnCompleteHandler(rotate => StartCoroutine(EndRotation(PlayerChar.X)));
			}
			
			yield return null;
		}
		
		yield return null;
	}
	
	bool canShoot = true;
	
	public float shootingInterval = 0.25f;
	
	IEnumerator EndRotation(PlayerChar newChar) {
			
		if(canShoot) {
			
			canShoot = false;	
			canRotate = true;
			currentCharacter = newChar;
			if(!root.isMoving) {
				Bullet tempBullet = Instantiate(bulletPrefab, gunPoint.position, 
					gunPoint.rotation) as Bullet;
				tempBullet.ColorIndex = (int)currentCharacter;
			}
			
			yield return new WaitForSeconds(shootingInterval);
			canShoot = true;
		}
		yield return null;
	}
	
	void FixedUpdate(){
		//rigidbody.MovePosition(input*Time.fixedDeltaTime*speed+transform.position);
	}
}
