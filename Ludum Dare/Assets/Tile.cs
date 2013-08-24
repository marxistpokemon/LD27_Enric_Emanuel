using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public Coord gridPos;
	public bool isOccuppied;
	
	// Use this for initialization
	void Start () {
		isOccuppied = false;
	}
}
