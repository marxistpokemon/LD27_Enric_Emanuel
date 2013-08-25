using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
//	public static GameManager reg;
	private static GameManager instance;
	
	
	public Material[] colors;
	
	public static GameManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
			}
			return instance;
		}
	}
	
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
